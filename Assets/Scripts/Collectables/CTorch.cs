using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CTorch : Singleton<CTorch> {
    [Header("Required Connections")]
    [SerializeField] private GameObject blackMask;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private GameObject camera;
    [SerializeField] private AudioManager audioManager;

    [Header("Torch Settings")]
    [SerializeField] private GameObject vendor;
    private Vendor vendorScript;
    [SerializeField] private Transform vendorPosition;
    [SerializeField] private float spawnOffsetX;
    [SerializeField] private float spawnOffsetY;
    [SerializeField] public float newViewDistance = 12.0f;
    [SerializeField] private float newCameraDistance = 6.5f;
    [SerializeField] private float torchFollowSpeed = 1.0f;
    [SerializeField] public float torchLerpTime = 2.0f;
    [SerializeField] private float alphaOscillationSpeed = 1.0f;
    [SerializeField] private float alphaOscillationMagnitude = 0.1f; // Adjust for desired effect
    private float baseAlpha = 0.85f; // The central value of alpha around which it oscillates
    [SerializeField] private float maxVol = 0.15f;
    [SerializeField] private float minVol = 0.015f;

    private float lastOffsetX;
    private float lastOffsetY;

    public bool torchHasSpawned;

    public bool sceneIsSame;
    public string currentScene;

    private readonly string TORCHKEY = "TORCH_KEY";

    protected override void Awake() {
        if (GameObject.FindObjectsOfType<CTorch>().Length > 1) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        base.Awake();

        // THIS IS JUST FOR TESTING PURPOSES
        //PlayerPrefs.SetInt(TORCHKEY, 0); 

        if (!GameManager.Instance.PLAYER_PREF_KEYS.Contains(TORCHKEY)) {
            GameManager.Instance.PLAYER_PREF_KEYS.Add(TORCHKEY);
        }

        if (audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
        }

        currentScene = SceneManager.GetActiveScene().name;
        sceneIsSame = true;
    }

    private void Start() {
        // Im not using the base class Start() method because I want to 
        // have it follow the player like the things in binding of isaac
        // (sorry if that doesn't make sense, I don't know how else to explain it)
        characterTransform = GameObject.Find("Player").transform;
        camera = GameObject.Find("Main Camera");

        //spriteRenderer = GetComponent<SpriteRenderer>();

        vendor = GameObject.Find("Vendor");
        vendorScript = vendor.GetComponent<Vendor>();
        vendorPosition = vendor.transform;

        transform.position = new Vector2(vendorPosition.position.x + spawnOffsetX, vendorPosition.position.y + spawnOffsetY);

        TorchRequirements();
    }

    private void EarlyUpdate() {
        if (!GameManager.Instance.PLAYER_PREF_KEYS.Contains(TORCHKEY)) {
            GameManager.Instance.PLAYER_PREF_KEYS.Add(TORCHKEY);
        }

        audioManager = FindObjectOfType<AudioManager>();
        fieldOfView = FindObjectOfType<FieldOfView>();
        blackMask = GameObject.Find("Black");
        characterTransform = GameObject.Find("Player").transform;
        camera = GameObject.Find("Main Camera");
        vendor = GameObject.Find("Vendor");
        vendorScript = vendor.GetComponent<Vendor>();
        vendorPosition = vendor.transform;
    }

    private void Update() {
        TorchRequirements();
    }

    private void TorchRequirements() {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "LoreScene")
        {
            // NO TORCH LOGIC ON MAIN MENU OR LORESCENE
            return;
        }
        if (audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if (fieldOfView == null) {
            fieldOfView = FindObjectOfType<FieldOfView>();
        }

        if (blackMask == null) {
            blackMask = GameObject.Find("Black");
        } else {
            if (fieldOfView.GetComponent<FieldOfView>().shouldUpdatePulseParameters && torchHasSpawned) {
                alphaOscillationSpeed = fieldOfView.GetComponent<FieldOfView>().pulseSpeed * 0.2f;
                alphaOscillationMagnitude = fieldOfView.GetComponent<FieldOfView>().pulseMagnitude * 0.2f;
                
                OscillateAlpha();
            }
        }
        if (characterTransform == null) {
            if (GameObject.Find("Player") != null) {
                characterTransform = GameObject.Find("Player").transform;
            }
        }

        if (vendor == null) {
            vendor = GameObject.Find("Vendor");
            if (vendor != null) {
                vendorScript = vendor.GetComponent<Vendor>();
                vendorPosition = vendor.transform;
            }
        }

        if (!sceneIsSame) {
            currentScene = SceneManager.GetActiveScene().name;
            sceneIsSame = true;

            audioManager = FindObjectOfType<AudioManager>();
            fieldOfView = FindObjectOfType<FieldOfView>();
            blackMask = GameObject.Find("Black");
            characterTransform = GameObject.Find("Player").transform;
            camera = GameObject.Find("Main Camera");
            vendor = GameObject.Find("Vendor");
            vendorScript = vendor.GetComponent<Vendor>();
            vendorPosition = vendor.transform;

            if (torchHasSpawned) {
                PlayerPrefs.SetInt(TORCHKEY, 1);
            }
        }

        if (vendorScript.torchBought || PlayerPrefs.GetInt(TORCHKEY) == 1) {
            vendorScript.torchBought = true;

            Debug.Log("Spawning torch");
            Debug.Log("Torch bought: " + vendorScript.torchBought);
            Debug.Log("torchkey pref: " + PlayerPrefs.GetInt(TORCHKEY));
            SpawnTorch();

            torchHasSpawned = true;

            PlayerPrefs.SetInt(TORCHKEY, 1);
        }

        if (torchHasSpawned) {
            if (audioManager != null && SceneManager.GetActiveScene().name != "MainMenu") {
                if (SceneManager.GetActiveScene().name == "LoreScene"){
                    audioManager.StopSFX("8BitFire");
                } else if (!audioManager.IsPlayingSFX("8BitFire")) {
                    AdjustFireSoundVolume();
                    audioManager.PlaySFX("8BitFire");
                }
            } else if (SceneManager.GetActiveScene().name == "MainMenu") {
                audioManager.StopSFX("8BitFire");
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0) {
                float offsetX = (horizontal < 0) ? Mathf.Floor(horizontal) : Mathf.Ceil(horizontal);
                float offsetY = (vertical < 0) ? Mathf.Floor(vertical) : Mathf.Ceil(vertical);

                transform.position = Vector2.Lerp(transform.position, characterTransform.position, torchFollowSpeed * Time.deltaTime);
                lastOffsetX = offsetX;
                lastOffsetY = offsetY;
            } else {
                if ((!(transform.position.x < lastOffsetX + 0.5f) || !(transform.position.y < lastOffsetY + 0.5f)) && characterTransform != null) {
                    transform.position = Vector2.Lerp(transform.position, new Vector2(characterTransform.position.x - lastOffsetX, characterTransform.position.y - lastOffsetY), torchFollowSpeed * Time.deltaTime);
                }
            }

            if (blackMask != null) {
                if (fieldOfView != null) {
                    if (fieldOfView.GetComponent<FieldOfView>().shouldUpdatePulseParameters && torchHasSpawned) {
                        alphaOscillationSpeed = fieldOfView.GetComponent<FieldOfView>().pulseSpeed * 0.1f;
                        alphaOscillationMagnitude = fieldOfView.GetComponent<FieldOfView>().pulseMagnitude * 0.1f;

                        OscillateAlpha();
                    }
                }
            }
        } else {
            vendor = GameObject.Find("Vendor");
            if (vendor != null) {
                vendorScript = vendor.GetComponent<Vendor>();
                vendorPosition = GameObject.Find("Vendor").transform;
                transform.position = new Vector2(vendorPosition.position.x + spawnOffsetX, vendorPosition.position.y + spawnOffsetY);
            }

            PlayerPrefs.SetInt(TORCHKEY, 0);
        }
    }

    private void LateUpdate() {
        if (currentScene != SceneManager.GetActiveScene().name) {
            sceneIsSame = false;
        }
    }

    public void SpawnTorch() {
        if (camera == null) {
            camera = GameObject.Find("Main Camera");
        } else {
            if (Camera.main.orthographicSize != newCameraDistance && camera.GetComponent<Camera2D>() != null) {
                camera.GetComponent<Camera2D>().originalZoom = newCameraDistance;
                camera.GetComponent<Camera2D>().vendorZoomOut = 1.5f;
                camera.GetComponent<Camera2D>().bossZoomOut = 1.9f;
            }
        }
    }

    private void OscillateAlpha() {
        SpriteRenderer blackMaskSpriteRenderer = blackMask.GetComponent<SpriteRenderer>();
        Color targetColor;

        if (blackMaskSpriteRenderer != null) {
            float oAlpha = baseAlpha + Mathf.Sin(alphaOscillationSpeed * Time.time) * alphaOscillationMagnitude;
            targetColor = new Color(blackMaskSpriteRenderer.color.r, blackMaskSpriteRenderer.color.g, blackMaskSpriteRenderer.color.b, oAlpha);
            blackMaskSpriteRenderer.color = Color.Lerp(blackMaskSpriteRenderer.color, targetColor, torchLerpTime * Time.deltaTime);
        } else {
            Debug.LogError("blackMask does not have a SpriteRenderer component.");
        }
    }

    private void AdjustFireSoundVolume() {
        if (!torchHasSpawned || characterTransform == null) {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, characterTransform.position);

        float maxDistance = 3.5f;

        distanceToPlayer = Mathf.Clamp(distanceToPlayer, 0, maxDistance);
        float volume = Mathf.Lerp(maxVol, minVol, distanceToPlayer / maxDistance);

        audioManager.SetSFXVolume("8BitFire", volume);
    }

    /*
    public void StartFadingAlpha(float targetAlpha, float duration) {
        SpriteRenderer blackMaskSpriteRenderer = blackMask.GetComponent<SpriteRenderer>();
        if (blackMaskSpriteRenderer != null) {
            StartCoroutine(FadeToAlpha(blackMaskSpriteRenderer, targetAlpha, duration));
        } else {
            Debug.LogError("blackMask does not have a SpriteRenderer component.");
        }
    }


    private IEnumerator FadeToAlpha(SpriteRenderer spriteRenderer, float targetAlpha, float duration) {
        float time = 0;
        Color startColor = spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (time < duration) {
            time += Time.deltaTime;
            float normalizedTime = time / duration; // This will go from 0 to 1, thus completing the fade.
            // Ensure the normalized time is not greater than 1.
            normalizedTime = Mathf.Clamp01(normalizedTime);

            if (spriteRenderer != null) {
                spriteRenderer.color = Color.Lerp(startColor, targetColor, normalizedTime);
            }
            yield return null;
        }
    }
    */

    public void Pick() {
        SpawnTorch();
    }

    private void OnQuit() {
        PlayerPrefs.SetInt(TORCHKEY, 0);
    }
}

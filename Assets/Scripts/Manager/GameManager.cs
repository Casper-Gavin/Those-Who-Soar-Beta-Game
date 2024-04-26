using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [Header("Scene Management")]
    [SerializeField] public string DEFAULT_SCENE;
    // player pref key for the current scene, hide in inspector
    [HideInInspector] public string CURRENT_SCENE;

    [Header("Player Pref Management")]
    // An object is a generic type that can hold any type of data
    [SerializeField] public List<object> PLAYER_PREF_KEYS = new List<object>();
    [SerializeField] public List<object> PLAYER_PREF_KEYS_UNCHANGEABLE = new List<object>();

    [SerializeField] private GameObject resetNotice;
    [SerializeField] private float showNoticeTime = 3f;
    private bool showNotice = false;

    private void Awake() {
        if (GameObject.FindObjectsOfType<GameManager>().Length > 1) {
            Destroy(gameObject);
            return;
        }

        base.Awake();

        DontDestroyOnLoad(gameObject);

        PlayerPrefs.SetString("DEFAULT_SCENE", DEFAULT_SCENE);
        //PlayerPrefs.SetString("CURRENT_SCENE", DEFAULT_SCENE);

        if (CURRENT_SCENE == null || CURRENT_SCENE == "") {
            if (PlayerPrefs.HasKey("CURRENT_SCENE")) {
                CURRENT_SCENE = PlayerPrefs.GetString("CURRENT_SCENE");
            } else {
                PlayerPrefs.SetString("CURRENT_SCENE", DEFAULT_SCENE);
                CURRENT_SCENE = DEFAULT_SCENE;
            }
        }

        // if CURRENT_SCENE isnt in the list, add it
        if (!PLAYER_PREF_KEYS.Contains("CURRENT_SCENE")) {
            PLAYER_PREF_KEYS.Add("CURRENT_SCENE");
        }

        if (!PLAYER_PREF_KEYS_UNCHANGEABLE.Contains("DEFAULT_SCENE")) {
            PLAYER_PREF_KEYS_UNCHANGEABLE.Add("DEFAULT_SCENE");
        }
    }

    private void Update() {
        if (CURRENT_SCENE == null || CURRENT_SCENE == "") {
            if (PlayerPrefs.HasKey("CURRENT_SCENE")) {
                CURRENT_SCENE = PlayerPrefs.GetString("CURRENT_SCENE");
            } else {
                PlayerPrefs.SetString("CURRENT_SCENE", DEFAULT_SCENE);
                CURRENT_SCENE = DEFAULT_SCENE;
            }
        }

        // set the current scene to the name of the current scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "LoreScene" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TutorialScene" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TestScene") {
            CURRENT_SCENE = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
        
        if (CURRENT_SCENE != PlayerPrefs.GetString("CURRENT_SCENE")) {
            // if the current scene is different than the saved scene, update the saved scene
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "LoreScene" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TutorialScene" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TestScene") {
                PlayerPrefs.SetString("CURRENT_SCENE", CURRENT_SCENE);
            }
        }

        // if the current scene is different than the saved scene, update the saved scene
        if (CURRENT_SCENE != PlayerPrefs.GetString("CURRENT_SCENE") && (CURRENT_SCENE != "MainMenu" || CURRENT_SCENE != "LoreScene" || CURRENT_SCENE != "TutorialScene" || CURRENT_SCENE != "TestScene")) {
            PlayerPrefs.SetString("CURRENT_SCENE", CURRENT_SCENE);
        }

        if (!PLAYER_PREF_KEYS.Contains("CURRENT_SCENE")) {
            PLAYER_PREF_KEYS.Add("CURRENT_SCENE");
        }

        if (!PLAYER_PREF_KEYS_UNCHANGEABLE.Contains("DEFAULT_SCENE")) {
            PLAYER_PREF_KEYS_UNCHANGEABLE.Add("DEFAULT_SCENE");
        }

        if (showNotice) {
            // count down the time to show the notice
            showNoticeTime -= Time.deltaTime;
        }

        if (resetNotice != null) {
            if (showNoticeTime <= 0 || UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu") {
                // if the time is up, hide the notice
                showNotice = false;
                showNoticeTime = 3f;
                resetNotice.SetActive(false);
            }
        }
    }

    private void OnApplicationQuit() {
        PlayerPrefs.SetString("CURRENT_SCENE", CURRENT_SCENE);
    }

    public void ResetPlayerPrefs() {
        // if a player pref is in the regular list, delete it
        
        PlayerPrefs.DeleteKey("TORCH_KEY");
        PlayerPrefs.DeleteKey("MyGame_MySkillPoints_DontCheat");
        PlayerPrefs.DeleteKey("MyGame_MySkillPointsTotal_DontCheat");
        PlayerPrefs.DeleteKey("SkillLevel_0");
        PlayerPrefs.DeleteKey("SkillLevel_1");
        PlayerPrefs.DeleteKey("SkillLevel_2");
        PlayerPrefs.DeleteKey("SkillLevel_3");
        PlayerPrefs.DeleteKey("SkillLevel_4");

        PlayerPrefs.DeleteKey("MyGame_MyCoins_DontCheat");

        foreach (object key in PLAYER_PREF_KEYS)
        {
            PlayerPrefs.DeleteKey(key.ToString());
        }

        // TODO: reset scene to restore
        PlayerPrefs.SetString("CURRENT_SCENE", DEFAULT_SCENE);
        CURRENT_SCENE = DEFAULT_SCENE;

        showNotice = true;
        if (!resetNotice)
        {
            resetNotice = GameObject.Find("ResetProgressNoticeWrapper").transform.GetChild(0).gameObject;
        }
        resetNotice.SetActive(true);
    }
}

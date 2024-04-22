using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using static SkillMenu;

public class UIManager : Singleton<UIManager>
{
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    [Header("Skill Tree")]
    [SerializeField] private GameObject skillTreeUI;
    public static bool SkillTreeIsOpen = false;

    [Header("Control Menu")]
    [SerializeField] private GameObject controlMenuUI;
    public static bool ControlMenuIsOpen = false;

    [Header("Settings")]
    [SerializeField] private Image damageIndicator;
    [SerializeField] private Image healthIndicator;
    [SerializeField] private Image shieldIndicator;
    [SerializeField] private Image coinIndicator;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;
    [SerializeField] private Image skillPointBar;

    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI currentAmmoTMP;
    [SerializeField] private Image weaponImage;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI currentHealthTMP;
    [SerializeField] private TextMeshProUGUI currentShieldTMP;
    [SerializeField] private TextMeshProUGUI coinsTMP;
    [SerializeField] private TextMeshProUGUI skillPointsTotalTMP;

    [Header("Boss")]
    [SerializeField] private Image bossHealthImage; // BossHealth
    [SerializeField] private GameObject bossHealthBarPanel; // HealthContainer
    [SerializeField] private GameObject bossIntroPanel; // BossIntro
    [SerializeField] private GameObject bossDoor; // spawns after boss entry
    [SerializeField] private GameObject bossKey; // spawns after boss entry

    [Header("Keys")]
    [SerializeField] private GameObject initialKeySpot;
    [SerializeField] private bool expandHorizontal = true;
    [SerializeField] private float keySpacer = 48.0f;
    [SerializeField] private Canvas canvas;
    
    [Header("Level Clear")]
    [SerializeField] private GameObject levelClearImage; // EndLevelImage - in canvas
    [SerializeField] private GameObject gameClearImage; // only for final level

    [SerializeField] public KeybindingActions keybindingActions;


    private float playerCurrentHealth;
    private float playerMaxHealth;
    private float playerCurrentShield;
    private float playerMaxShield;
    private float playerCurrentSkillPoints;
    private int playerCurrentSkillPointsTotal;
    private float playerMaxSkillPoints;
    private int playerSPCounter;
    private bool isPlayer;

    private int playerCurrentAmmo;
    private int playerMaxAmmo;

    private float bossCurrentHealth;
    private float bossMaxHealth;

    private SkillMenu skillMenu;

    private List<Key> keys;
    private List<GameObject> keyImages;

    public bool isStart = true;

    private void Start()
    {
        isStart = true;

        Color c = damageIndicator.color;
        c.a = 0;
        damageIndicator.color = c;

        c = healthIndicator.color;
        c.a = 0;
        healthIndicator.color = c;

        c = shieldIndicator.color;
        c.a = 0;
        shieldIndicator.color = c;

        c = coinIndicator.color;
        c.a = 0;
        coinIndicator.color = c;

        playerMaxSkillPoints = 100f; // SkillPoints to get 1 skill point
        //playerTotalSkillPoints = SkillPointManager.Instance.SkillPointsTotal;
        playerSPCounter = -1;
        //SkillPointManager.Instance.ResetSkillPoints(); // For testing only

        skillMenu = SkillMenu.skillMenu;

        initialKeySpot.SetActive(false);
        keys = new List<Key>();
        keyImages = new List<GameObject>();
    }

    private void Update()
    {
        if (InputManager.instance.GetKeyDown(KeybindingActions.Pause)) {
            if (!SkillTreeIsOpen && !ControlMenuIsOpen) {
                if (GameIsPaused) {
                    Resume();
                } else {
                    Pause();
                }
            }
        }

        if (InputManager.instance.GetKeyDown(KeybindingActions.SkillMenu)) {
            if (!GameIsPaused) {
                if (SkillTreeIsOpen) {
                    SkillMenuClose();
                } else {
                    SkillMenuOpen();
                }
            }
        }

        InternalUpdate();

        // if it has been 2 seconds since the game started, then we can change the bool
        if (isStart && Time.timeSinceLevelLoad > 2) {
            isStart = false;
        }
    }

    public void FlashDamageEffect()
    {
        damageIndicator.enabled = true;
        Color c = damageIndicator.color;
        c.a = 1;
        damageIndicator.color = c;
    }

    public void FlashHealthEffect()
    {
        healthIndicator.enabled = true;
        Color c = healthIndicator.color;
        c.a = 1;
        healthIndicator.color = c;
    }

    public void FlashShieldEffect()
    {
        shieldIndicator.enabled = true;
        Color c = shieldIndicator.color;
        c.a = 1;
        shieldIndicator.color = c;
    }

    public void FlashCoinEffect()
    {
        coinIndicator.enabled = true;
        Color c = coinIndicator.color;
        c.a = 1;
        coinIndicator.color = c;
    }

    public void UpdateHealth(float currentHealth, float maxHealth, float currentShield, float maxShield)
    {
        playerCurrentHealth = currentHealth;
        playerMaxHealth = maxHealth;
        playerCurrentShield = currentShield;
        playerMaxShield = maxShield;
    }

    public void UpdateBossHealth (float currentHealth, float maxHealth) {
        bossCurrentHealth = currentHealth;
        bossMaxHealth = maxHealth;
    }

    public void UpdateWeaponSprite(Sprite weaponSprite) {
        weaponImage.sprite = weaponSprite;
        //weaponImage.SetNativeSize();
    }

    public void HideAmmo()
    {
        currentAmmoTMP.enabled = false;
    }

    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        currentAmmoTMP.enabled = true;
        playerCurrentAmmo = currentAmmo;
        playerMaxAmmo = maxAmmo;
    }

    private void InternalUpdate()
    {
        // PLAYER HEALTH
        // to make health bar update smoothly, we lerp
        // visually it will look smooth, but each time it comes in here it just moves a little more
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerCurrentHealth / playerMaxHealth, 10f * Time.deltaTime);
        currentHealthTMP.text = playerCurrentHealth.ToString() + "/" + playerMaxHealth.ToString();
        shieldBar.fillAmount = Mathf.Lerp(shieldBar.fillAmount, playerCurrentShield / playerMaxShield, 10f * Time.deltaTime);
        currentShieldTMP.text = playerCurrentShield.ToString() + "/" + playerMaxShield.ToString();

        skillPointBar.fillAmount = Mathf.Lerp(skillPointBar.fillAmount, SkillPointManager.Instance.SkillPoints / playerMaxSkillPoints, 10f * Time.deltaTime);
        playerCurrentSkillPoints = SkillPointManager.Instance.SkillPoints;
        playerCurrentSkillPointsTotal = SkillPointManager.Instance.SkillPointsTotal;

        // PLAYER SKILL POINTS
        if (playerCurrentSkillPoints >= playerMaxSkillPoints && playerCurrentSkillPoints != 0 && playerSPCounter <= playerCurrentSkillPointsTotal) {
            playerCurrentSkillPointsTotal ++;
            playerCurrentSkillPoints = 0;

            skillPointBar.fillAmount = Mathf.Lerp(skillPointBar.fillAmount, SkillPointManager.Instance.SkillPoints / playerMaxSkillPoints, 10f * Time.deltaTime);
            SkillPointManager.Instance.SkillPoints = 0;
        }

        skillPointsTotalTMP.text = playerCurrentSkillPointsTotal.ToString();
        skillMenu.skillPoints = playerCurrentSkillPointsTotal;
        SkillPointManager.Instance.SkillPointsTotal = playerCurrentSkillPointsTotal;
        SkillPointManager.Instance.SaveSkillPoints();

        // DAMAGE INDICATOR
        if (damageIndicator.enabled)
        {
            Color c = damageIndicator.color;
            c.a = Mathf.Lerp(c.a, 0, 5f * Time.deltaTime);
            damageIndicator.color = c;
            if (c.a == 0)
            {
                damageIndicator.enabled = false;
            }
        }

        // HEALTH INDICATOR
        if (healthIndicator.enabled)
        {
            Color c = healthIndicator.color;
            c.a = Mathf.Lerp(c.a, 0, 5f * Time.deltaTime);
            healthIndicator.color = c;
            if (c.a == 0)
            {
                healthIndicator.enabled = false;
            }
        }

        // SHIELD INDICATOR
        if (shieldIndicator.enabled)
        {
            Color c = shieldIndicator.color;
            c.a = Mathf.Lerp(c.a, 0, 5f * Time.deltaTime);
            shieldIndicator.color = c;
            if (c.a == 0)
            {
                shieldIndicator.enabled = false;
            }
        }

        // COIN INDICATOR
        if (coinIndicator.enabled)
        {
            Color c = coinIndicator.color;
            c.a = Mathf.Lerp(c.a, 0, 5f * Time.deltaTime);
            coinIndicator.color = c;
            if (c.a == 0)
            {
                coinIndicator.enabled = false;
            }
        }

        // SHIELD REGEN INDICATOR
        // should be the same as shield indicator, but it will change the alpha value from 0.3 to 1 steadily until shield is full
        // then it will disappear

        if (shieldIndicator.enabled)
        {
            Color c = shieldIndicator.color;
            c.a = Mathf.Lerp(c.a, 0, 5f * Time.deltaTime);
            shieldIndicator.color = c;
            if (c.a == 0)
            {
                shieldIndicator.enabled = false;
            }
        }


        // PLAYER AMMO
        currentAmmoTMP.text = playerCurrentAmmo + " / " + playerMaxAmmo;

        // PLAYER COINS
        coinsTMP.text = CoinManager.Instance.Coins.ToString();

        // boss health update
        bossHealthImage.fillAmount = Mathf.Lerp(bossHealthImage.fillAmount, bossCurrentHealth / bossMaxHealth, 10f * Time.deltaTime);
    }

    private void OnApplicationQuit() {
        SkillPointManager.Instance.SaveSkillPoints();
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    public void Pause() {
        bossIntroPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
    }

    public void SkillMenuOpen() {
        skillTreeUI.SetActive(true);
        Time.timeScale = 0f;
        SkillTreeIsOpen = true;
        Cursor.visible = true;
    }

    public void SkillMenuClose() {
        skillTreeUI.SetActive(false);
        Time.timeScale = 1f;
        SkillTreeIsOpen = false;
        Cursor.visible = false;
    }

    public void ControlMenuOpen() {
        controlMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        ControlMenuIsOpen = true;
        Cursor.visible = true;
    }

    public void ControlMenuClose() {
        controlMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        ControlMenuIsOpen = false;
        Cursor.visible = false;
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadMainMenu() {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void SetBossHealthBarVisible(bool visible)
    {
        bossHealthBarPanel.SetActive(visible);
    }

    private void BossFight() {
        bossIntroPanel.SetActive(true);
        StartCoroutine(MyLibrary.FadeCanvasGroup(bossIntroPanel.GetComponent<CanvasGroup>(), 1f, 1f, () => {
            bossHealthBarPanel.SetActive(true);
            StartCoroutine(MyLibrary.FadeCanvasGroup(bossHealthBarPanel.GetComponent<CanvasGroup>(), 1f, 1f));
        }));
    }

    private void BossFightStart() {
        bossDoor.SetActive(true);
        bossKey.SetActive(true);
        StartCoroutine(MyLibrary.FadeCanvasGroup(bossIntroPanel.GetComponent<CanvasGroup>(), 0.5f, 0f, () => {
            bossIntroPanel.SetActive(false);
        }));
    }

    public void OnBossDead() {
        StartCoroutine(MyLibrary.FadeCanvasGroup(bossHealthBarPanel.GetComponent<CanvasGroup>(), 1f, 0f, () => {
            bossHealthBarPanel.SetActive(false);
            gameClearImage.SetActive(true);
        }));
    }

    public IEnumerator LevelClearTutorial() {
        levelClearImage.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator LevelClearOne() {
        levelClearImage.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LevelTwoScene");
    }

    public IEnumerator LevelClearTwo() {
        levelClearImage.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LevelThreeScene");
    }

    public IEnumerator LevelClearThree() {
        levelClearImage.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LevelOneScene"); // change to "LevelFourScene" when 4 is created
    }

    public IEnumerator LevelClearFour() {
        levelClearImage.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LevelFiveScene");
    }

    // subscribe to event
    private void OnEnable() {
        GameEvent.OnEventFired += OnEventResponse;
        BossHealth.OnBossDead += OnBossDead;

    }

    // unsubscribe to event
    private void OnDisable() {
        GameEvent.OnEventFired -= OnEventResponse;
        BossHealth.OnBossDead -= OnBossDead;
    }

    private void OnEventResponse(GameEvent.EventType obj) {
        switch (obj) {
            case GameEvent.EventType.BossFightIntro:
                BossFight();
                break;
            case GameEvent.EventType.BossFightStart:
                BossFightStart();
                break;
            case GameEvent.EventType.LevelClearTutorial:
                StartCoroutine(LevelClearTutorial());
                break;
            case GameEvent.EventType.LevelClearOne:
                StartCoroutine(LevelClearOne());
                break;
            case GameEvent.EventType.LevelClearTwo:
                StartCoroutine(LevelClearTwo());
                break;
            case GameEvent.EventType.LevelClearThree:
                StartCoroutine(LevelClearThree());
                break;
            case GameEvent.EventType.LevelClearFour:
                StartCoroutine(LevelClearFour());
                break;
        }
    }

    public void AddKey(Key k)
    {
        GameObject image = Instantiate(initialKeySpot) as GameObject;
        image.transform.SetParent(canvas.transform.Find("BarsContainer").transform, false);
        image.GetComponent<Image>().sprite = k.GetKeyImageForUI();
        image.SetActive(true);
        if (expandHorizontal)
        {
            image.transform.position = new Vector3(image.transform.position.x + keys.Count * keySpacer, 
                                                   image.transform.position.y, 
                                                   image.transform.position.z);
        }
        else
        {
            image.transform.position = new Vector3(image.transform.position.x, 
                                                   image.transform.position.y + keys.Count * keySpacer, 
                                                   image.transform.position.z);
        }
        keys.Add(k);
        keyImages.Add(image);
    }

    public void RemoveKey(Key k)
    {
        int index = -1;
        for (int i = 0; i < keys.Count; i++)
        {
            if (k == keys[i])
            {
                index = i;
            }
        }
        if (index == -1)
        {
            Debug.Log("Error: Tried to remove a key that doesn't exist on the UI side!");
            return;
        }

        keys.Remove(keys[index]);
        Destroy(keyImages[index]);
        keyImages.RemoveAt(index);

        // shift all other keys after the one removed
        for (int i = index; i < keyImages.Count; i++)
        {
            if (expandHorizontal)
            {
                keyImages[i].transform.position = new Vector3(keyImages[i].transform.position.x - keySpacer, 
                                                              keyImages[i].transform.position.y, 
                                                              keyImages[i].transform.position.z);
            }
            else
            {
                keyImages[i].transform.position = new Vector3(keyImages[i].transform.position.x, 
                                                              keyImages[i].transform.position.y - keySpacer, 
                                                              keyImages[i].transform.position.z);
            }
        }
    }
}

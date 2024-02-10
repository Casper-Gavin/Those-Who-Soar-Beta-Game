using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class UIManager : Singleton<UIManager>
{
    public InputMaster controls;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    [Header("Skill Tree")]
    [SerializeField] private GameObject skillTreeUI;
    public static bool SkillTreeIsOpen = false;

    [Header("Settings")]
    [SerializeField] private Image damageIndicator;
    [SerializeField] private Image healthIndicator;
    [SerializeField] private Image shieldIndicator;
    [SerializeField] private Image coinIndicator;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;
    [SerializeField] private TextMeshProUGUI currentHealthTMP;
    [SerializeField] private TextMeshProUGUI currentShieldTMP;

    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI currentAmmoTMP;
    [SerializeField] private Image weaponImage;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI coinsTMP;

    [Header("Boss")]
    [SerializeField] private Image bossHealthImage;
    [SerializeField] private GameObject bossHealthBarPanel;
    [SerializeField] private GameObject bossIntroPanel;


    private float playerCurrentHealth;
    private float playerMaxHealth;
    private float playerCurrentShield;
    private float playerMaxShield;
    private bool isPlayer;

    private int playerCurrentAmmo;
    private int playerMaxAmmo;

    private float bossCurrentHealth;
    private float bossMaxHealth;

    private void Start()
    {
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!SkillTreeIsOpen) {
                if (GameIsPaused) {
                    Resume();
                } else {
                    Pause();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            if (!GameIsPaused) {
                if (SkillTreeIsOpen) {
                    SkillMenuClose();
                } else {
                    SkillMenuOpen();
                }
            }
        }

        InternalUpdate();
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
        weaponImage.SetNativeSize();
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

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SkillMenuOpen() {
        skillTreeUI.SetActive(true);
        Time.timeScale = 0f;
        SkillTreeIsOpen = true;
    }

    public void SkillMenuClose() {
        skillTreeUI.SetActive(false);
        Time.timeScale = 1f;
        SkillTreeIsOpen = false;
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadMainMenu() {
        Resume();
        SceneManager.LoadScene(0);
    }

    private void BossFight() {
        bossIntroPanel.SetActive(true);
        bossHealthBarPanel.SetActive(true);
    }

    private void BossFightStart() {
        bossIntroPanel.SetActive(false);
    }

    // subscribe to event
    private void OnEnable() {
        GameEvent.OnEventFired += OnEventResponse;
    }

    // unsubscribe to event
    private void OnDisable() {
        GameEvent.OnEventFired -= OnEventResponse;
    }

    private void OnEventResponse(GameEvent.EventType obj) {
        switch (obj) {
            case GameEvent.EventType.BossFightIntro:
                BossFight();
                break;
            case GameEvent.EventType.BossFightStart:
                BossFightStart();
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : HealthBase
{
    [SerializeField] private GameObject enemyHealthBarPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, 0f);
    [SerializeField] private int damageTakenFromBullet = 1;
    private Image healthBarImage;
    private GameObject gameObjectHealthBar;

    protected override void Awake() // this used to be Start(), it is apparently very weird to use Start() with MonoBehavior inheritance
    {
        // Instantiate is used to create game objects (prefabs) on the fly
        gameObjectHealthBar = Instantiate(enemyHealthBarPrefab, transform.position + offset, Quaternion.identity);
        gameObjectHealthBar.transform.parent = transform;
        healthBarImage = gameObjectHealthBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        CurrentHealth = initialHealth;
    }

    protected override void Update()
    {
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(damageTakenFromBullet);
        }
    }

    protected override void UpdateHealth()
    {
        healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, CurrentHealth / maxHealth, 10f * Time.deltaTime);
    }
}
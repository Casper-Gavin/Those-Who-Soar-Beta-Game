using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject enemyHealthBarPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, 0f);
    [SerializeField] private int damageToApply = 1;
    private Health enemyHealth;
    private Image enemyHealthBar;
    private GameObject enemyBar;

    private float enemyCurrentHealth;
    private float enemyMaxHealth;

    private void Start()
    {
        enemyHealth = GetComponent<Health>();
        // Instantiate is used to create game objects (prefabs) on the fly
        enemyBar = Instantiate(enemyHealthBarPrefab, transform.position + offset, Quaternion.identity);
        enemyBar.transform.parent = transform;
        enemyHealthBar = enemyBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(damageToApply);
        }
    }

    private void Update()
    {
        UpdateHealth();
    }

    private void TakeDamage(int damage)
    {
        enemyHealth.TakeDamage(damage);
    }

    private void UpdateHealth()
    {
        enemyHealthBar.fillAmount = Mathf.Lerp(enemyHealthBar.fillAmount, enemyCurrentHealth / enemyMaxHealth, 10f * Time.deltaTime);
    }

    public void UpdateEnemyHealth(float current, float max)
    {
        enemyCurrentHealth = current;
        enemyMaxHealth = max;
    }
}

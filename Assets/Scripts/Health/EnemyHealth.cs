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

    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private int numberOfFlashes = 4;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] public float explosionDamage = 5f;
    [SerializeField] private GameObject explosionEffectPrefab;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private int currentFlashCount = 0;
    public bool IsDead;

    protected override void Awake() // this used to be Start(), it is apparently very weird to use Start() with MonoBehavior inheritance
    {
        // Instantiate is used to create game objects (prefabs) on the fly
        gameObjectHealthBar = Instantiate(enemyHealthBarPrefab, transform.position + offset, Quaternion.identity);
        gameObjectHealthBar.transform.parent = transform;
        healthBarImage = gameObjectHealthBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        CurrentHealth = initialHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        IsDead = false;
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

    protected override void Die()
    {
        if (gameObject.CompareTag("Bomber"))
        {
            IsDead = true;
            TriggerFlash();
        }
        else
        {
            DestroyObject();
        }
    }

    private void TriggerFlash() 
    {
        if (currentFlashCount < numberOfFlashes)
        {
            StartCoroutine(Flash());
        }
        else
        {
            spriteRenderer.color = originalColor;
            
            Explode();
        }
    }

    private IEnumerator Flash()
    {
        float lerpTime = flashDuration / 2;
        float time = 0;

        while (time < lerpTime)
        {
            spriteRenderer.color = Color.Lerp(originalColor, flashColor, time / lerpTime);
            time += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = flashColor;
        time = 0;

        while (time < lerpTime)
        {
            spriteRenderer.color = Color.Lerp(flashColor, originalColor, time / lerpTime);
            time += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor;
        currentFlashCount++;
        TriggerFlash();
    }

    private void Explode()
    {
        // Instantiate the explosion effect
        if (explosionEffectPrefab)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth>().TakeDamage((int)explosionDamage);
            }
        }

        DestroyObject();
    }

    void OnDrawGizmos()
    {
        // Draw a red sphere in the editor to visualize explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
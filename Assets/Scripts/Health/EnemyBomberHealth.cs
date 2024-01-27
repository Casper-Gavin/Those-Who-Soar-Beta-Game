using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBomberHealth : EnemyHealth
{
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

    protected override void Awake()
    {
        base.Awake();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        IsDead = false;
    }

    protected override void Die()
    {
        IsDead = true;
        TriggerFlash();
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
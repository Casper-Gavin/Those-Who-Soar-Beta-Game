using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBase : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Sprite damagedSprite;

    [Header("Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private bool isDamageable;

    private ComponentHealth health;
    private SpriteRenderer spriteRenderer;
    private JarReward jarReward;
    private Collider2D collider2D;
    

    private void Start() {
        health = GetComponent<ComponentHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jarReward = GetComponent<JarReward>();
        collider2D = GetComponent<Collider2D>();
    }

    // using health for non-player object
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            TakeDamage();
        }
    }

    // checks if component is destroyed and if it needs to replace or get rid of component sprite
    // only bullet needs to have Is Trigger on - only one object in a collision
    private void TakeDamage() {
        health.TakeDamage(damage);

        if (health.CurrentHealth > 0) {
            if (isDamageable) {
                spriteRenderer.sprite = damagedSprite;
            }
        }

        if (health.CurrentHealth <= 0) {
            if (isDamageable) {
                // Box
                Destroy(gameObject);
            } else {
                // Jar
                spriteRenderer.sprite = damagedSprite;
                collider2D.enabled = false;
                jarReward.GiveReward();
            }
        }
    }
}

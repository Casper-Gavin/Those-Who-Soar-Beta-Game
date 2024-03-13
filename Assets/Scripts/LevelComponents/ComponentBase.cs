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
    [SerializeField] private float boxShiftAmount = 0.1f;

    private ComponentHealth health;
    private SpriteRenderer spriteRenderer;
    private JarReward jarReward;
    private Collider2D collider2D;
    
    [SerializeField] private AudioManager audioManager;

    private void Start() {
        health = GetComponent<ComponentHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jarReward = GetComponent<JarReward>();
        collider2D = GetComponent<Collider2D>();

        audioManager = AudioManager.Instance;
    }

    private void Update() {
    }

    // using health for non-player object
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet") || other.CompareTag("PlayerSword"))
        {
            TakeDamage(other.transform.position.x > transform.position.x);

            if (this.gameObject.tag == "BoxComponent" && audioManager != null) {
                int random = Random.Range(1, 3);

                if (random == 1) {
                    audioManager.PlaySFX("HitBox1");
                } else if (random == 2) {
                    audioManager.PlaySFX("HitBox2");
                } else {
                    audioManager.PlaySFX("HitBox3");
                }
            } else if (this.gameObject.tag == "JarComponent" && audioManager != null) {
                audioManager.MakeAndPlaySFX("JarSmash");
            } else {
                if (audioManager != null) {
                    // Add sword hitting wall sound?
                    // audioManager.PlaySFX("SwordHit");
                }
            }
        }
    }

    // checks if component is destroyed and if it needs to replace or get rid of component sprite
    // only bullet needs to have Is Trigger on - only one object in a collision
    private void TakeDamage(bool left) {
        health.TakeDamage(damage);

        if (health.CurrentHealth > 0) {
            if (isDamageable) {
                spriteRenderer.sprite = damagedSprite;
                StartCoroutine(ShakeBox(left));
            }
        }

        if (health.CurrentHealth <= 0) {
            if (isDamageable) {
                // Box
                Destroy(gameObject);

                if (this.gameObject.tag == "BoxComponent" && audioManager != null) {
                    audioManager.PlaySFX("BoxBreak");
                }
            } else {
                // Jar
                spriteRenderer.sprite = damagedSprite;
                collider2D.enabled = false;
                jarReward.GiveReward();
            }
        }
    }

    private IEnumerator ShakeBox(bool left)
    {
        float shiftAmount = left ? boxShiftAmount : -boxShiftAmount;
        // shift left
        transform.position = new Vector3(transform.position.x - shiftAmount, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.1f);
        // return to normal position
        transform.position = new Vector3(transform.position.x + shiftAmount, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.1f);
        // shift right
        transform.position = new Vector3(transform.position.x + shiftAmount, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(0.1f);
        // return to normal position
        transform.position = new Vector3(transform.position.x - shiftAmount, transform.position.y, transform.position.z);
    }
}

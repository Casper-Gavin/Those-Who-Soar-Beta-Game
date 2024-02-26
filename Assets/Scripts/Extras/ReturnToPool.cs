using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private LayerMask objectMask;
    [SerializeField] private float lifeTime = 2f;

    [SerializeField] private ParticleSystem impactPS;

    private Projectile projectile;
    private BossProjectile bossProjectile;

    [SerializeField] private AudioManager audioManager;

    private void Start () {
        projectile = GetComponent<Projectile>();
        bossProjectile = GetComponent<BossProjectile>();

        if (audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    private void Update() {
        if (audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    private void Return () {
        if (projectile != null) {
            projectile.ResetProjectile();
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MyLibrary.CheckLayer(other.gameObject.layer, objectMask))
        {
            if (projectile != null)
            {
                projectile.DisableProjectile();

                if (other.gameObject.tag == "BoxComponent" && audioManager != null) {
                    int random = Random.Range(1, 3);

                    if (random == 1) {
                        audioManager.PlaySFX("HitBox1");
                    } else if (random == 2) {
                        audioManager.PlaySFX("HitBox2");
                    } else {
                        audioManager.PlaySFX("HitBox3");
                    }
                } else if (other.gameObject.tag == "JarComponent" && audioManager != null) {
                    audioManager.PlaySFX("JarSmash");
                } else {
                    if (audioManager != null) {
                        audioManager.PlaySFX("BulletHit");
                    }
                }
            }

            if (bossProjectile != null) {
                bossProjectile.DisableBossProjectile();
            }

            impactPS.Play();
            Invoke(nameof(Return), impactPS.main.duration);
        }
    }

    // only uses the lifetime if it isn't 0 (or less)
    private void OnEnable() {
        if (lifeTime > 0) {
            Invoke(nameof(Return), lifeTime);
        }
    }

    private void OnDisable() {
        CancelInvoke();
    }
}

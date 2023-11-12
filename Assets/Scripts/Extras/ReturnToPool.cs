using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private LayerMask objectMask;
    [SerializeField] private float lifeTime = 2f;

    [SerializeField] private ParticleSystem impactPS;

    private Projectile projectile;

    private void Start () {
        projectile = GetComponent<Projectile>();
    }

    private void Return () {
        if (projectile != null) {
            projectile.ResetProjectile();
        }

        gameObject.SetActive(false);
    }

    private bool CheckLayer(int layer, LayerMask objectMask)
    {
        return ((1 << layer) & objectMask) != 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckLayer(other.gameObject.layer, objectMask))
        {
            if (projectile != null)
            {
                projectile.DisableProjectile();
            }
            impactPS.Play();
            Invoke(nameof(Return), impactPS.main.duration);
        }
    }

    private void OnEnable() {
        Invoke(nameof(Return), lifeTime);
    }

    private void OnDisable() {
        CancelInvoke();
    }
}

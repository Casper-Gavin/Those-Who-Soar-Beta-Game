using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRandomPattern : BossBaseShot
{
    [Header("Random Settings")]
    [Range(0, 360f)] [SerializeField] private float startAngle = 180f;
    [Range(0, 360f)] [SerializeField] private float range = 360f;

    [SerializeField] private float minRandomSpeed = 1f;
    [SerializeField] private float maxRandomSpeed = 3f;

    [SerializeField] private float minDelay = 0.01f;
    [SerializeField] private float maxDelay = 0.1f;

    private float nextShotTime;
    private int shotIndex;


    protected override void Start()
    {
        base.Start();
        EnableShooting();
    }

    private void Update() {
        nextShotTime = 0f;
        Shoot();
    }

    private void Shoot() {
        if (isShooting == false) {
            return;
        }

        // sets up the fixed time for each shot to go off - extra check - same as spiral pattern
        if (nextShotTime >= 0f) {
            nextShotTime -= Time.deltaTime;

            if (nextShotTime >= 0f) {
                return;
            }
        }

        BossProjectile bossProjectile = GetBossProjectile(transform.position);

        if (bossProjectile == null) {
            return;
        }

        // calculates/creates the random speed and angle
        float speed = Random.Range(minRandomSpeed, maxRandomSpeed);

        float minAngle = startAngle - range / 2f;
        float maxAngle = startAngle + range / 2f;
        float angle = Random.Range(minAngle, maxAngle);
        
        // actual shot
        ShootProjectile(bossProjectile, speed, angle, projectileAcceleration);
        shotIndex++;

        // checks if all projectiles have been shot
        if (shotIndex >= projectileAmount) {
            DisableShooting();
        } else {
            nextShotTime = Time.time + Random.Range(minDelay, maxDelay);
            
            if (nextShotTime <= 0) {
                Update();
            }
        }
    }
}

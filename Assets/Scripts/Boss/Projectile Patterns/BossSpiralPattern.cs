using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiralPattern : BossBaseShot
{
    [Range(0f, 360f)][SerializeField] private float startAngle = 180f;
    [Range(-360f, 360f)][SerializeField] private float shiftAngle = 5f;
    [SerializeField] private float shotDelay = 0.5f;

    private int shotIndex;
    private float nextShotTime;


    private void Update() {
        nextShotTime = 0f;
        Shoot();
    }

    private void Shoot() {
        if (isShooting == false) {
            return;
        }

        // sets up the fixed time for each shot to go off - extra check
        if (nextShotTime >= 0f) {
            nextShotTime -= Time.deltaTime;

            if (nextShotTime >= 0f) {
                return;
            }
        }

        // gets the projectile
        BossProjectile bossProjectile = GetBossProjectile(transform.position);

        // gets the angle
        float angle = startAngle + shiftAngle * shotIndex;

        // shoots the projectile at the angle
        ShootProjectile(bossProjectile, projectileSpeed, angle, projectileAcceleration);

        // increase shot index
        shotIndex++;

        if (shotIndex >= projectileAmount) {
            DisableShooting();
        } else {
            nextShotTime = Time.time + shotDelay;
            
            if (nextShotTime <= 0) {
                Update();
            }
        }
    }

    // shoot again with the shot index reset
    public void EnableProjectile() {
        isShooting = true;
        shotIndex = 0;
    }
}

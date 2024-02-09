using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Boss/Actions/Shoot Player", fileName = "ActionBossShootPlayer")]
public class ActionBossShootPlayer : AIAction
{
    public float shotDelay = 3f;
    private float nextShotTime;


    public override void Init(StateController controller) {
        // do nothing - currently only used in explody enemy
    }

    public override void Act(StateController controller)
    {
        Shoot(controller);
    }

    // uses a different shot pattern depending on the player's current health - easier or harder to kill player?
    private void Shoot(StateController controller) {
        if (Time.time > nextShotTime) {
            if (controller.PlayerHealth.CurrentHealth >= 7) {
                controller.BossSpiralPattern.EnableProjectile();
            }

            if (controller.PlayerHealth.CurrentHealth >= 4 && controller.PlayerHealth.CurrentHealth < 7) {
                controller.BossRandomPattern.EnableProjectile();
            }

            if (controller.PlayerHealth.CurrentHealth <= 4) {
                controller.BossCirclePattern.EnableProjectile();
            }

            nextShotTime = Time.time + shotDelay;
        }
    }

    private void OnEnable() {
        nextShotTime = 0;
    }
}

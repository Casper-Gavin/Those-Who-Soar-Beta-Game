using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : EnemyHealth
{
    // Awake is called before start
    protected override void Awake()
    {
        base.Awake();

        //ShowBossHealth(true);
    }

    protected override void UpdateHealth()
    {
        UIManager.Instance.UpdateBossHealth(CurrentHealth, maxHealth);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(damageTakenFromBullet);
        }
    }

    protected override void Die()
    {
        //ShowBossHealth(false);
        base.Die();
    }

}
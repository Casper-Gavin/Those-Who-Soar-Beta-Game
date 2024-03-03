using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : EnemyHealth
{
    public static Action OnBossDead;

    // Awake is called before start
    protected override void Awake()
    {
        initialHealth = maxHealth;

        base.Awake();
    }

    protected override void UpdateHealth()
    {
        UIManager.Instance.UpdateBossHealth(CurrentHealth, maxHealth);
    }

    // public override void TakeDamage(int damage) {
    //     if (CurrentHealth <= 0)
    //     {
    //         return;
    //     }

    //     CurrentHealth -= damage;
        
    //     if (skillMenu.skillLevels[(int)SkillMenu.SkillEnum.IncreaseDamage] > 0)
    //     {
    //         CurrentHealth -= skillMenu.skillLevels[(int)SkillMenu.SkillEnum.IncreaseDamage];
    //     }
        
    //     CurrentHealth = Mathf.Max(CurrentHealth, 0); // prevent negative numbers
    //     UpdateHealth();

    //     if (CurrentHealth <= 0) {
    //         Debug.Log("Die?");
    //         Die();
    //     }
    // }

    protected override void Die()
    {
        OnBossDead?.Invoke();
        UIManager.Instance.OnBossDead();
        base.Die();
    }

}
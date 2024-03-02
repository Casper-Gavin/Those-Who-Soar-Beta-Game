using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : EnemyHealth
{
    [SerializeField] protected float initialHealth = 100f;
    [SerializeField] protected float maxHealth = 100f;

    public static Action OnBossDead;

    // Awake is called before start
    protected override void Awake()
    {
        maxHealth = 100f;
        initialHealth = maxHealth;

        base.Awake();
    }

    protected override void UpdateHealth()
    {
        UIManager.Instance.UpdateBossHealth(CurrentHealth, this.maxHealth);
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
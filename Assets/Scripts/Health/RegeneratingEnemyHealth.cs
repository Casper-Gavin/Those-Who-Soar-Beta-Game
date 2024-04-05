using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegeneratingEnemyHealth : EnemyHealth
{
    [SerializeField] protected float regenRate = 0.5f;

    protected override void Update()
    {
        if (CurrentHealth < maxHealth)
        {
            CurrentHealth += regenRate * Time.deltaTime;
            if (CurrentHealth >= maxHealth)
            {
                CurrentHealth = maxHealth;
            }
            UpdateHealth();
        }
    }
}
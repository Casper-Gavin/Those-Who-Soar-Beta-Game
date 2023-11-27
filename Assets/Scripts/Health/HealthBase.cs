using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float initialHealth = 10f;
    [SerializeField] protected float maxHealth = 10f;
    

    [Header("Settings")]
    [SerializeField] protected bool destroyObject;

    public float CurrentHealth { get; set; }

    protected virtual void Awake()
    {
        CurrentHealth = initialHealth;

        UpdateHealth();
    }

    protected virtual void Update()
    {
        // For testing only
        /*if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }*/
    }

    public virtual void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0); // prevent negative numbers
        UpdateHealth();

        if (CurrentHealth == 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (destroyObject)
        {
            DestroyObject();
        }
    }

    public virtual void Revive()
    {
        gameObject.SetActive(true);
        CurrentHealth = initialHealth;

        UpdateHealth();
    }

    public virtual void GainHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        UpdateHealth();
    }

    protected virtual void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    protected abstract void UpdateHealth();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] protected string weaponName = "";

    [Header("Settings")]
    [SerializeField] protected float attackCooldown = 0.5f;
    [SerializeField] protected Sprite weaponUISprite; 

    public string WeaponName => weaponName;

    protected PlayerController controller;
    public Character WeaponOwner { get; set; }

    public WeaponAim WeaponAim { get; set; }

    public bool OffAttackCooldown { get; set; }

    protected float nextAttackTime;

    protected virtual void Awake()
    {
        WeaponAim = GetComponent<WeaponAim>();
    }

    protected virtual void Update()
    {
        CheckWeaponCooldown();
        FlipWeapon();
    }

    public abstract void Attack();
    public abstract void StopAttack();
    protected abstract void RequestAttack();
    public abstract void EquipWeapon();
    public abstract void HolsterWeapon();

    // Reload will not be applicable for every weapon, but since we need to call it
    // regardless when the reload key is pressed, unconcerned weappons should just
    // stub this function out
    public abstract void Reload(); 

    protected virtual void CheckWeaponCooldown()
    {
        if (Time.time > nextAttackTime)
        {
            OffAttackCooldown = true;
        }
    }

    protected virtual void FlipWeapon()
    {
        if (WeaponOwner)
        {
            bool right = WeaponOwner.GetComponent<CharacterFlip>().FacingRight;
            transform.localScale = new Vector3(right ? 1 : -1, 1, 1);
        }
    }

    public virtual void SetOwner(Character owner)
    {
        WeaponOwner = owner;
        controller = WeaponOwner.GetComponent<PlayerController>();
    }
}
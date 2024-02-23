using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS CODE IS CURRENTLY UNDER THE ASSUMPTION 
// THAT THE ATTACK COOLDOWN TIME IS THE SAME AS THE ANIMATION TIME
// if this assumption does not work, add an extra delay variable and
// use that to control the box collider toggling
public class MeleeWeapon : WeaponBase
{
    private BoxCollider2D boxCollider;
    private bool currentlyAttacking = false;

    private Animator animator;
    private readonly int useMeleeWeapon = Animator.StringToHash("UseMeleeWeapon");

    protected override void Awake() // this was Start, see if Awake works
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        base.Awake();
    }

    public override void Attack()
    {
        RequestAttack();
    }
    
    public override void StopAttack()
    {
        //Debug.Log("Attack stopped");
        boxCollider.enabled = false;
    }

    public override void Reload()
    {
        // do nothing
    }

    public override void EquipWeapon()
    {
        // add equip animation maybe?
        // use WeaponOwner
        if (WeaponOwner.CharacterTypes == Character.CharacterTypeEnum.Player)
        {
            UIManager.Instance.HideAmmo();
            if (weaponUISprite != null)
            {
                UIManager.Instance.UpdateWeaponSprite(weaponUISprite);
            }
            else
            {
                UIManager.Instance.UpdateWeaponSprite(gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
            }
        }
    }

    public override void HolsterWeapon()
    {
        // do nothing for now
        // maybe trigger animation in the future
    }

    protected override void CheckWeaponCooldown()
    {
        if (Time.time > nextAttackTime)
        {
            boxCollider.enabled = false; // attack finished,
            currentlyAttacking = false;
            OffAttackCooldown = true;
        }
    }

    // TODO: test this, logic may be a little fuzzy
    protected override void RequestAttack()
    {
        if (currentlyAttacking)
        {
            return;
        }

        boxCollider.enabled = true; // disable once contact is made or cooldown expires
        if (OffAttackCooldown)
        {
            currentlyAttacking = true;
            animator.SetTrigger(useMeleeWeapon);
            // set animation length to be the same as attack speed??

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    protected override void FlipWeapon()
    {
        /*if (WeaponOwner.CharacterTypes == Character.CharacterTypeEnum.Player)
        {*/
            bool right = WeaponOwner.GetComponent<CharacterFlip>().FacingRight;
            Vector3 newScale = transform.localScale;
            //Vector3 newPosition = transform.position;
            if (right)
            {
                newScale.x = Mathf.Abs(newScale.x);
                //newPosition.x = WeaponOwner.transform.position.x + Mathf.Abs(WeaponOwner.transform.position.x - newPosition.x);
            }
            else
            {
                newScale.x = -Mathf.Abs(newScale.x);
                //newPosition.x = WeaponOwner.transform.position.x - Mathf.Abs(WeaponOwner.transform.position.x - newPosition.x);
            }
            //transform.position = newPosition;
            transform.localScale = newScale;
        //}
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float attackDelay = 0.5f;
    private bool isAttacking;
    private BoxCollider2D boxCollider;

    private readonly int useMeleeWeapon = Animator.StringToHash("UseMeleeWeapon"); // TODO: this ok?

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
        FlipMeleeWeapon();
    }

    public override void UseWeapon()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (isAttacking)
        {
            yield break;
        }

        boxCollider.enabled = false;
        isAttacking = true;
        animator.SetTrigger(useMeleeWeapon);

        yield return new WaitForSeconds(attackDelay);
        boxCollider.enabled = true;
        isAttacking = false;
    }

    private void FlipMeleeWeapon()
    {
        if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
}
*/
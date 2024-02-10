using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] public int damageToEnemy;

    private SkillMenu skillMenu;
    private int prevDamage;

    private void Awake()
    {
        damageToEnemy = 2;

        skillMenu = SkillMenu.skillMenu;
        prevDamage = 0;
    }

    private void LateUpdate()
    {
        if (prevDamage < skillMenu.skillLevels[(int)SkillMenu.SkillEnum.IncreaseDamage])
        {
            damageToEnemy = damage + skillMenu.skillLevels[(int)SkillMenu.SkillEnum.IncreaseDamage];
            prevDamage = skillMenu.skillLevels[(int)SkillMenu.SkillEnum.IncreaseDamage];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // make this work with any component with a health
        // but wait, we don't want it to be able
        // to damage other enemies? Maybe a layer thing, like for bullets
        // check!!!!
        //other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        if (gameObject.layer == 9 /* enemy hit player*/)
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            // cancel sword collider so no multiple damages on one swing
            gameObject.GetComponent<MeleeWeapon>().StopAttack();
        }
        // level component damage is even more custom, handled in componentBase
        // TODO: may want to redo componentbase so that we can remove the if statement here
        // and move componentbase TakeDamage to ComponentHealth, but will need a lot of references
        // that we might not want inside of ComponentHealth
        else if (other.gameObject.layer != 7 /* player hit level component */)
        {
            other.GetComponent<HealthBase>()?.TakeDamage(damage);
        }
        else if (other.gameObject.layer == 9 /* player hit enemy */)
        {
            other.GetComponent<HealthBase>()?.TakeDamage(damageToEnemy);
            // cancel sword collider (can't double attack enemies)

            gameObject.GetComponent<MeleeWeapon>().StopAttack();
        }
    }
}

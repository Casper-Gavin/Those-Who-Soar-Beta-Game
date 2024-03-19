using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillMenu;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private int damage;

    [Range(0f, 100f)]
    [SerializeField] public float critChance;

    private SkillMenu skillMenu;

    private void Awake()
    {
        skillMenu = SkillMenu.skillMenu;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.layer == 9 /* enemy hit player*/)
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            // cancel sword collider so no multiple damages on one swing
            gameObject.GetComponent<MeleeWeapon>().StopAttack();
        }
        else if (other.gameObject.layer == 9 /* player hit enemy */)
        {
            int totalDamage = damage + skillMenu.skillLevels[(int)SkillEnum.IncreaseDamage];
            if (Random.Range(0, 100) < critChance)
            {
                totalDamage += totalDamage / 2; // crit damage
                other.GetComponent<HealthBase>()?.TakeDamage(totalDamage);
            }
            else
            {
                other.GetComponent<HealthBase>()?.TakeDamage(totalDamage);
            }

            // cancel sword collider (can't double attack enemies)
            gameObject.GetComponent<MeleeWeapon>().StopAttack();
        }
        // level component damage is even more custom, handled in componentBase
        // TODO: may want to redo componentbase so that we can remove the if statement here
        // and move componentbase TakeDamage to ComponentHealth, but will need a lot of references
        // that we might not want inside of ComponentHealth
        else if (other.gameObject.layer != 7 /* player hit level component */)
        {
            other.GetComponent<HealthBase>()?.TakeDamage(1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private Character.CharacterTypeEnum damageType = Character.CharacterTypeEnum.Enemy;
    [SerializeField] private int damageToApply = 1;

    private PlayerHealth playerHealth;
    //private EnemyHealth enemyHealth;
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            if (other.GetComponent<Projectile>().ProjectileOwner.CharacterTypes == damageType)
            {
                playerHealth.TakeDamage(damageToApply);
            }
        } /*
        else if (other.CompareTag("Bomber") && enemyHealth.IsDead == true)
        {
            playerHealth.TakeDamage((int)enemyHealth.explosionDamage);
        }*/
    }
}

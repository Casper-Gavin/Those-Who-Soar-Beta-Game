using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private Character.CharacterType damageType = Character.CharacterType.Enemy;
    [SerializeField] private int damageToApply = 1;

    private PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.GetComponent<Projectile>().ProjectileOwner.CharacterTypes == damageType)
            {
                playerHealth.TakeDamage(damageToApply);
            }
        }
    }
}

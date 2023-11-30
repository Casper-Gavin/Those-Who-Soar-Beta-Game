using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // make this work with any component with a health
        // but wait, we don't want it to be able
        // to damage other enemies? Maybe a layer thing, like for bullets
        // check!!!!
        other.GetComponent<HealthBase>()?.TakeDamage(damage);
    }
}

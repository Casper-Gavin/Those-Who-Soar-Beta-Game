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
        //other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        if (gameObject.layer == 9 /* enemy */)
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
        // level component damage is even more custom, handled in componentBase
        // TODO: may want to redo componentbase so that we can remove the if statement here
        // and move componentbase TakeDamage to ComponentHealth, but will need a lot of references
        // that we might not want inside of ComponentHealth
        else if (other.gameObject.layer != 7 /* level component */)
        {
            other.GetComponent<HealthBase>()?.TakeDamage(damage);
        }
    }
}

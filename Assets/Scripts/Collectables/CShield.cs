using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShield : Collectables {
    [SerializeField] private int shieldToAdd = 1;
    [SerializeField] private ParticleSystem shieldBonus;

    protected override void Pick() {
        AddShield(character);
    }

    protected override void PlayEffects() {
        Instantiate(shieldBonus, transform.position, Quaternion.identity);
    }

    // takes in a character and public to allow for adding shield in other classes
    public void AddShield(Character characterHealth) {
        characterHealth.GetComponent<PlayerHealth>().GainShield(shieldToAdd);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeapon : Collectables
{
    [SerializeField] private ItemData itemWeaponData;

    protected override void Pick() {
        EquipWeapon();
    }

    // adds a secondary weapon - jumps through some hoops
    private void EquipWeapon() {
        if (character != null) {
            CharacterWeapon characterWeapon = character.GetComponent<CharacterWeapon>();
            characterWeapon.SecondaryWeapon = itemWeaponData.WeaponToEquip;
            if (characterWeapon.SecondaryEquipped) // re-equip
            {
                characterWeapon.EquipWeapon(characterWeapon.SecondaryWeapon);
            }
            characterWeapon.information = null; // force a full reset on equipped weapon
        }
    }
}

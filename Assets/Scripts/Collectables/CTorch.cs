using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTorch : Collectables
{
    protected override void Pick() {
        //EquipTorch(character);
    }

    protected override void PlayEffects()
    {
        // don't know if we want torch effects - maybe a flame/fire or something?
    }

    private void EquipTorch() {
        // what will go in here depends on whether we just want the torch to change some FOV values (look at CShield/CHealth)
        // or:
        // equip an actual torch (look at CWeapon)
    }
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class WeaponAmmoTest
{
    [Test]
    public void TestConsumeAmmo()
    {
        GameObject gun = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Weapons/PlayerWeapons/Weapon_Special.prefab"), 
                                     new Vector3(0, 0, 0), 
                                     Quaternion.identity);
        gun.GetComponent<WeaponAim>().InjectPlayer = true;
        gun.GetComponent<WeaponAmmo>().ConsumeAmmo();
        Assert.AreEqual(gun.GetComponent<WeaponAmmo>().GetComponent<ProjectileWeapon>().CurrentAmmo, 29);
    }
}

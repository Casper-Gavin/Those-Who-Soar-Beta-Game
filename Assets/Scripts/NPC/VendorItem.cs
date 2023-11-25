using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vendor/Item")]
public class VendorItem : ScriptableObject
{
    public Collectables itemCollectable;
    public Weapon WeaponToSell;
    public int Cost;

}

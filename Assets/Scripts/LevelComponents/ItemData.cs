using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// allows us to create a item weapon object in unity
[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Item Weapon")]
// inherits from scriptable object -  don't create prefabs, create a script that stores data
public class ItemData : ScriptableObject
{
    public Weapon WeaponToEquip;
    public Sprite WeaponSprite;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    private ProjectileWeapon weapon;

    private readonly string WEAPON_AMMO_SAVELOAD = "Weapon_";

    private void Awake()
    {
        weapon = GetComponent<ProjectileWeapon>();
        LoadWeaponAmmoSize();
    }

    public void ConsumeAmmo()
    {
        if (weapon.UseMagazine)
        {
            weapon.CurrentAmmo -= 1;
        }
    }

    // checks for ammo
    public bool CanUseWeapon()
    {
        if (weapon.CurrentAmmo > 0 && UIManager.GameIsPaused == false)
        {
            return true;
        }

        return false;
    }

    public void RefillAmmo()
    {
        if (weapon.UseMagazine && UIManager.GameIsPaused == false)
        {
            weapon.CurrentAmmo = weapon.MagazineSize;
        }
    }

    // loads currently equipped weapon ammo - different than refill ammo
    public void LoadWeaponAmmoSize() {
        int savedAmmo = LoadAmmo();
        weapon.CurrentAmmo = savedAmmo < weapon.MagazineSize ? LoadAmmo() : weapon.MagazineSize;
    }

    public void SaveAmmo() {
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + weapon.WeaponName, weapon.CurrentAmmo);

        if (!GameManager.Instance.PLAYER_PREF_KEYS.Contains(WEAPON_AMMO_SAVELOAD + weapon.WeaponName)) {
            GameManager.Instance.PLAYER_PREF_KEYS.Add(WEAPON_AMMO_SAVELOAD + weapon.WeaponName);
        }
    }

    public int LoadAmmo() {
        if (!GameManager.Instance.PLAYER_PREF_KEYS.Contains(WEAPON_AMMO_SAVELOAD + weapon.WeaponName)) {
            GameManager.Instance.PLAYER_PREF_KEYS.Add(WEAPON_AMMO_SAVELOAD + weapon.WeaponName);
        }
        
        // returns the saved ammo or the magazine size if the 1st argument (the string) is null
        return PlayerPrefs.GetInt(WEAPON_AMMO_SAVELOAD + weapon.WeaponName, weapon.MagazineSize);
    }
}

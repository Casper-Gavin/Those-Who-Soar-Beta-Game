using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterWeapon : CharacterAbilities
{
    public static Action OnStartShooting;

    [Header("Weapon Settings")]
    [SerializeField] private Weapon weaponToUse;
    [SerializeField] private Transform weaponHolderPosition;

    public Weapon CurrentWeapon { get; set; }

    public Weapon SecondaryWeapon { get; set; }

    public WeaponAim WeaponAim { get; set; }

    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse, weaponHolderPosition);
    }

    // left mouse is shoot, R is reload, more to come
    protected override void HandleInput()
    {
        if (character.CharacterTypes == Character.CharacterType.Player)
        {
            // 0 is left mouse button - GetMouseButtonDown makes a non-auto weapon vs GetMouseButton which is auto
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopWeapon();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
            // Aplha1 is 1 on num pad - && stops equiping if there's no secondary weapon
            if (Input.GetKeyDown(KeyCode.Alpha1) && SecondaryWeapon != null)
            {
                EquipWeapon(weaponToUse, weaponHolderPosition);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && SecondaryWeapon != null)
            {
                EquipWeapon(SecondaryWeapon, weaponHolderPosition);
            }
        }
    }

    public void Shoot()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.UseWeapon();
        if (character.CharacterTypes == Character.CharacterType.Player)
        {
            OnStartShooting?.Invoke();

            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    public void StopWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.StopWeapon();
    }

    public void Reload()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Reload();
        
        if (character.CharacterTypes == Character.CharacterType.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        if (CurrentWeapon != null) {
            // destroys the current reticle, projectile pool, and weapon after saving the weapon ammo
            CurrentWeapon.WeaponAmmo.SaveAmmo();
            WeaponAim.DestroyReticle();
            Destroy(GameObject.Find("Pool"));
            Destroy(CurrentWeapon.gameObject);
        }
        // creates reference to weapon to be used by player
        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition;
        CurrentWeapon.SetOwner(character);
        WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();

        if (character.CharacterTypes == Character.CharacterType.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
            UIManager.Instance.UpdateWeaponSprite(CurrentWeapon.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeBtwShots = 0.5f;
    
    [Header("Weapon")]
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true;


    public Character WeaponOwner { get; set; }

    public int CurrentAmmo { get; set; }

    public WeaponAmmo WeaponAmmo { get; set; }

    public bool UseMagazine => useMagazine;
    /* above same as on newer version of C#:
    public bool UseMagazine
    {
        get { return use Magazine;}
    }
    */

    public int MagazineSize => magazineSize;

    public bool CanShoot { get; set; }

    private float nextShotTime;

    private void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        CurrentAmmo = magazineSize;
    }

    private void Update()
    {
        WeaponCanShoot();
        Debug.Log(CurrentAmmo);
    }

    // TriggerShot calls StartShooting only because this is a generic weapon
    public void TriggerShot()
    {
        StartShooting();
    }

    private void StartShooting()
    {
        if (useMagazine)
        {
            if (WeaponAmmo != null)
            {
                if (WeaponAmmo.CanUseWeapon())
                {
                    RequestShot();
                }
                else
                {
                    if (autoReload)
                    {
                        Reload();
                    }
                }
            }
        }
        else
        {
            RequestShot();
        }
    }

    private void RequestShot()
    {
        if (!CanShoot)
        {
            return;
        }

        Debug.Log("Shooting");
        WeaponAmmo.ConsumeAmmo();

        CanShoot = false;
    }

    private void WeaponCanShoot()
    {
        if (Time.time > nextShotTime)
        {
            CanShoot = true;
            nextShotTime = Time.time + timeBtwShots;
        }
    }

    public void SetOwner(Character owner)
    {
        WeaponOwner = owner;
    }

    public void Reload()
    {
        if (WeaponAmmo != null)
        {
            if (useMagazine)
            {
                WeaponAmmo.RefillAmmo();
            }
        }
    }
}

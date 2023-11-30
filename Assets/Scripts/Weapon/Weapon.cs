/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] private string weaponName = "";

    [Header("Settings")]
    [SerializeField] private float timeBtwShots = 0.5f;
    
    [Header("Weapon")]
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true;

    [Header("Recoil")]
    [SerializeField] private bool useRecoil = true;
    [SerializeField] private int recoilForce = 5;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzlePS;

    public string WeaponName => weaponName;

    public Character WeaponOwner { get; set; }

    public int CurrentAmmo { get; set; }

    public WeaponAmmo WeaponAmmo { get; set; }

    public WeaponAim WeaponAim { get; set; }

    public bool UseMagazine => useMagazine;
    // above same as on newer version of C#:
    //public bool UseMagazine
    //{
    //    get { return use Magazine;}
    //}
    

    public int MagazineSize => magazineSize;

    public bool CanShoot { get; set; }

    private float nextShotTime;
    private PlayerController controller;
    protected Animator animator;
    private readonly int weaponUseParamater = Animator.StringToHash("weaponUse");

    protected virtual void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        animator = GetComponent<Animator>();
        WeaponAim = GetComponent<WeaponAim>();
    }

    protected virtual void Update()
    {
        WeaponCanShoot();
        RoatateWeapon();
    }

    // UseWeapon calls StartShooting only because this is a generic weapon
    public virtual void UseWeapon()
    {
        StartShooting();
    }

    public void StopWeapon()
    {
        if (useRecoil)
        {
            controller.ApplyRecoil(Vector2.one, 0f);
        }
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

    // checks cooldown time before shooting
    protected virtual void RequestShot()
    {
        if (!CanShoot)
        {
            return;
        }

        if (useRecoil)
        {
            Recoil();
        }

        animator.SetTrigger(weaponUseParamater);
        WeaponAmmo.ConsumeAmmo();
        muzzlePS.Play();
    }

    private void Recoil()
    {
        if (WeaponOwner != null)
        {
            if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            {
                controller.ApplyRecoil(Vector2.left, recoilForce);
            }
            else
            {
                controller.ApplyRecoil(Vector2.right, recoilForce);
            }
        }
    }

    protected virtual void WeaponCanShoot()
    {
        if (Time.time > nextShotTime)
        {
            CanShoot = true;
            nextShotTime = Time.time + timeBtwShots;
        }
    }

    // pass by reference
    public void SetOwner(Character owner)
    {
        WeaponOwner = owner;
        controller = WeaponOwner.GetComponent<PlayerController>();
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

    protected virtual void RoatateWeapon()
    {
        if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
}
*/
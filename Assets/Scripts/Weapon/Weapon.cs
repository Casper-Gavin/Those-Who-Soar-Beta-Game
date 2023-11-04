using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeBtwShoots = 0.5f;
    
    [Header("Weapon")]
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true;


    public Character WeaponOwner { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // TriggerShot calls StartShooting only because this is a generic weapon
    public void TriggerShot()
    {
        StartShooting();
    }

    private void StartShooting()
    {
        Debug.Log("Shooting");
    }

    public void SetOwner(Character owner)
    {
        WeaponOwner = owner;
    }

    public void Reload()
    {
        Debug.Log("Reload");
    }
}

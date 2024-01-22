using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Vendor : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("Items")]
    [SerializeField] private VendorItem weaponItem;
    [SerializeField] private VendorItem healthItem;
    [SerializeField] private VendorItem shieldItem;


    public bool canOpenShop;
    private CharacterWeapon characterWeapon;

    public UnityEvent OnPlayerEnterShopZone;
    public UnityEvent OnPlayerExitShopZone;

    protected Character character;

    // open and close shop panel
    private void Update() {
        if (canOpenShop) {
            if(Input.GetKeyDown(KeyCode.J)) {
                shopPanel.SetActive(true);
                popUpPanel.SetActive(false);
            }
        }

        if (shopPanel.activeInHierarchy) {
            BuyItems();
        }
    }

    // buy secondary gun (for now)
    private void BuyItems() {
        if (Input.GetKeyDown(KeyCode.M)) {
            if (CoinManager.Instance.Coins >= weaponItem.Cost) {
                characterWeapon.SecondaryWeapon = weaponItem.WeaponToSell;
                if (characterWeapon.SecondaryEquipped) // re-equip
                {
                    characterWeapon.EquipWeapon(characterWeapon.SecondaryWeapon);
                }
                ProductBought(weaponItem.Cost);
            }
        }

        // buy shield - checks for coin amount and then checks if the player has max shield (buying shield has no point)
        if (Input.GetKeyDown(KeyCode.N)) {
            if (CoinManager.Instance.Coins >= shieldItem.Cost) {
                if (characterWeapon.GetComponent<Character>().GetComponent<PlayerHealth>().CurrentShield != 5f) {
                    shieldItem.shieldItem.AddShield(characterWeapon.GetComponent<Character>());
                    ProductBought(shieldItem.Cost);
                }
            }
        }

        //buy health
        if (Input.GetKeyDown(KeyCode.B)) {
            if (CoinManager.Instance.Coins >= healthItem.Cost) {
                if (characterWeapon.GetComponent<Character>().GetComponent<PlayerHealth>().CurrentHealth != 10f) {
                    healthItem.healthItem.AddHealth(characterWeapon.GetComponent<Character>());
                    ProductBought(healthItem.Cost);
                }
            }
        }
    }

    // entering and exiting the vendor's RigidBody 2D's trigger zone
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            characterWeapon = other.GetComponent<CharacterWeapon>();
            canOpenShop = true;
            popUpPanel.SetActive(true); 
            OnPlayerEnterShopZone.Invoke();
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            characterWeapon = null;
            canOpenShop = false;
            popUpPanel.SetActive(false);
            shopPanel.SetActive(false);
            OnPlayerExitShopZone.Invoke();
        }

    }

    private void ProductBought(int amount) {
        shopPanel.SetActive(false);
        CoinManager.Instance.RemoveCoins(amount);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("Items")]
    [SerializeField] private VendorItem weaponItem;
    [SerializeField] private VendorItem healthItem;
    [SerializeField] private VendorItem shieldItem;

    private bool canOpenShop;
    private CharacterWeapon characterWeapon;


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

        if (Input.GetKeyDown(KeyCode.N)) {
            if (CoinManager.Instance.Coins >= shieldItem.Cost) {
                shieldItem.shieldItem.AddShield(characterWeapon.GetComponent<Character>());
                ProductBought(shieldItem.Cost);
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            if (CoinManager.Instance.Coins >= healthItem.Cost) {
                healthItem.healthItem.AddHealth(characterWeapon.GetComponent<Character>());
                ProductBought(healthItem.Cost);
            }
        }
    }

    // entering and exiting the vendor's RigidBody 2D's trigger zone
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            characterWeapon = other.GetComponent<CharacterWeapon>();
            canOpenShop = true;
            popUpPanel.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            characterWeapon = null;
            canOpenShop = false;
            popUpPanel.SetActive(false);
            shopPanel.SetActive(false);
        }

    }

    private void ProductBought(int amount) {
        shopPanel.SetActive(false);
        CoinManager.Instance.RemoveCoins(amount);
    }
}

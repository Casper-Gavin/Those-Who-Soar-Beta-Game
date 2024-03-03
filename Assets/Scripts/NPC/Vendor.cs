using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

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
    public string vendorTag;

    protected Character character;

    private void Awake() {
    }

    // open and close shop panel
    private void Update() {
        if (canOpenShop) {
            if(Input.GetKeyDown(KeyCode.J)) {
                CanBuyProduct();
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
            vendorTag = "Vendor";
            popUpPanel.SetActive(true); 
            OnPlayerEnterShopZone.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            characterWeapon = null;
            canOpenShop = false;
            vendorTag = null;
            popUpPanel.SetActive(false);
            shopPanel.SetActive(false);
            OnPlayerExitShopZone.Invoke();
        }

    }

    private void ProductBought(int amount) {
        shopPanel.SetActive(false);
        CoinManager.Instance.RemoveCoins(amount);

        AudioManager.Instance.PlaySFX("VendorBuy");
    }

    // change color of cost text - red (can't buy) or yellow (can buy)
    public void CanBuyProduct() {
        if (CoinManager.Instance.Coins >= shieldItem.Cost) {
            // yellow
            GameObject Tmp = shopPanel.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
            ChangeProductColor(Tmp, 'y');
        } else {
            // red
            GameObject Tmp = shopPanel.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
            ChangeProductColor(Tmp, 'r');
        }

        if (CoinManager.Instance.Coins >= healthItem.Cost) {
            // yellow
            GameObject Tmp = shopPanel.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            ChangeProductColor(Tmp, 'y');
        } else {
            // red
            GameObject Tmp = shopPanel.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            ChangeProductColor(Tmp, 'r');        }

        if (CoinManager.Instance.Coins >= weaponItem.Cost) {
            // yellow
            GameObject Tmp = shopPanel.transform.GetChild(0).GetChild(2).GetChild(0).gameObject;
            ChangeProductColor(Tmp, 'y');
        } else {
            // red
            GameObject Tmp = shopPanel.transform.GetChild(0).GetChild(2).GetChild(0).gameObject;
            ChangeProductColor(Tmp, 'r');
        }
    }

    public void ChangeProductColor(GameObject tmp, char color) {
        switch (color) {
            case 'y':
              tmp.GetComponent<TextMeshProUGUI>().color = new Color32(227, 229, 40, 255);
              break;
            case 'r':
                tmp.GetComponent<TextMeshProUGUI>().color = new Color32(229, 46, 40, 255);
                break;
        } 
    }
 }

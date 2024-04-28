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
    [SerializeField] private VendorItem torchItem;

    [Header("Dialogue")]
    [SerializeField] public DialogueTrigger dialogueTrigger;
    private string[] vendorNames;
    public string vendorName;
    private string[] vendorDialogues;
    public string vendorDialogue;


    public bool canOpenShop;
    private CharacterWeapon characterWeapon;

    public UnityEvent OnPlayerEnterShopZone;
    public UnityEvent OnPlayerExitShopZone;
    public string vendorTag;
    public bool torchBought;

    protected Character character;

    private void Awake() {
        CaptureDialogueValues();
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
                characterWeapon.information = null; // force a full reset on equipped weapon
                ProductBought(weaponItem.Cost);
            }
        }

        /*
        // buy shield - checks for coin amount and then checks if the player has max shield (buying shield has no point)
        if (Input.GetKeyDown(KeyCode.N)) {
            if (CoinManager.Instance.Coins >= shieldItem.Cost) {
                if (characterWeapon.GetComponent<Character>().GetComponent<PlayerHealth>().CurrentShield != 5f) {
                    shieldItem.shieldItem.AddShield(characterWeapon.GetComponent<Character>());
                    ProductBought(shieldItem.Cost);
                }
            }
        }
        */

        // buy torch
        if (!torchBought) {
            if (Input.GetKeyDown(KeyCode.N)) {
                if (CoinManager.Instance.Coins >= torchItem.Cost) {
                    torchBought = true;

                    ProductBought(torchItem.Cost);
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
            CaptureDialogueValues();
            dialogueTrigger.TriggerDialogue();
            OnPlayerEnterShopZone.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            characterWeapon = null;
            canOpenShop = false;
            vendorTag = null;
            if (shopPanel.activeSelf) {
                shopPanel.SetActive(false);
            }
            popUpPanel.SetActive(false);
            dialogueTrigger.EndDialogue();
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
        if (CoinManager.Instance.Coins >= torchItem.Cost) {
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

    public string GetVendorName() {
        vendorNames = new string[] {
            "Balthazar",
            "Cedric",
            "Darius",
            "Ezekiel",
            "Felix",
            "Gideon",
            "Hector",
            "Icarus",
            "Jasper",
            "Kendrick",
            "Lysander",
            "Marius",
            "Nathaniel",
            "Oberon",
            "Percival",
            "Quincy",
            "Roderick",
            "Sebastian",
            "Theodore",
            "Ulysses",
            "Valerian",
            "Wolfgang",
            "Xander",
            "Yorick",
            "Zephyr"
        };

        int randomIndex = UnityEngine.Random.Range(0, vendorNames.Length);

        return "Vendor " + vendorNames[randomIndex];
    }

    public string GetVendorDialogue() {
        vendorDialogues = new string[] {
            "Ah, a new face! Welcome! What can I do for you today?",
            "Step right up! Step right up! See what wonders I have in store!",
            "Everything's for sale, my friend. Everything. If I had a sister, I'd sell her in a second!",
            "Ah, you're back! I knew you couldn't stay away from my fabulous deals!",
            "Be careful with anything you touch! Oh, who am I kidding? If you break it, you buy it!",
            "You look like someone who knows the value of a good bargain.",
            "Rare goods from the farthest reaches of the land, available right here for the right price.",
            "My wares are guaranteed to bring you victory in battle... or your next of kin gets a full refund.",
            "Looking for something specific, or just browsing for treasures?",
            "Hurry up! These deals won't last forever... or will they? No, they won't. Or... maybe?",
            "You won't find merchandise like this anywhere else. Mostly because no one else would bother selling it.",
            "Come, come! Don't be shy. The best goods in all the land, right here in my humble shop.",
            "Remember, I'm not just a vendor. I'm your gateway to unparalleled power! ...And knick-knacks.",
            "Every item has a story. Buy one, and you'll be buying a piece of history. No refunds on the history, though.",
            "Gold spent here is gold well spent. At least, that's what I tell all my customers.",
            "Ah, take your time. Shopping is an art, not a race... Unless there's a sale. Then it's definitely a race.",
            "You look like someone on an important quest. How about some supplies to help you on your way?"
        };

        int randomIndex = UnityEngine.Random.Range(0, vendorDialogues.Length);

        return vendorDialogues[randomIndex];
    }

    public void CaptureDialogueValues() {
        vendorName = GetVendorName();
        vendorDialogue = GetVendorDialogue();

        dialogueTrigger.SetName(vendorName);
        dialogueTrigger.SetSentences(new string[] { vendorDialogue });
    }
}
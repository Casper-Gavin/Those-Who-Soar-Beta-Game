using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject shopPanel;

    private bool canOpenShop;
    void Start() {
        
    }

    private void Update() {
        if (canOpenShop) {
            if(Input.GetKeyDown(KeyCode.J)) {
                shopPanel.SetActive(true);
                popUpPanel.SetActive(false);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canOpenShop = true;
            popUpPanel.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canOpenShop = false;
            popUpPanel.SetActive(false);
            shopPanel.SetActive(false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoin : Collectables {
    [SerializeField] private int coinsToAdd = 20;

    protected override void Pick() {
        AddCoins();
    }

    private void AddCoins() {
        CoinManager.Instance.AddCoins(coinsToAdd);
    }

    private void RemoveCoins() {
        
    }
}

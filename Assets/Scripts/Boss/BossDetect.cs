using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossDetect : MonoBehaviour {
    public UnityEvent OnPlayerEnterBossZone;
    public UnityEvent OnPlayerExitBossZone;

    public bool canLeaveBossZone;
    public bool isInBossZone;
    public string bossTag;


    private void Awake() {
        canLeaveBossZone = true;
        isInBossZone = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canLeaveBossZone = false;
            isInBossZone = true;
            bossTag = "Boss";
            OnPlayerEnterBossZone.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canLeaveBossZone = true;
            isInBossZone = false;
            bossTag = null;
            OnPlayerExitBossZone.Invoke();
        }
    }
}

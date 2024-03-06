using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossDetect : MonoBehaviour {
    public UnityEvent OnPlayerEnterBossZone;
    public UnityEvent OnPlayerExitBossZone;
    public UnityEvent OnBossDead;

    public bool canLeaveBossZone;
    public bool isInBossZone;
    public string bossTag;
    public bool isBossDead;


    private void Awake() {
        canLeaveBossZone = true;
        isInBossZone = false;
    }

    private void Update() {
        if (isInBossZone && canLeaveBossZone) {
            OnBossDead.Invoke();
            isBossDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canLeaveBossZone = false;
            isInBossZone = true;
            bossTag = "Boss";
            OnPlayerEnterBossZone.Invoke();
            GameObject.FindObjectOfType<UIManager>().SetBossHealthBarVisible(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canLeaveBossZone = true;
            isInBossZone = false;
            bossTag = null;
            OnPlayerExitBossZone.Invoke();
            GameObject.FindObjectOfType<UIManager>().SetBossHealthBarVisible(false);
        }
    }
}

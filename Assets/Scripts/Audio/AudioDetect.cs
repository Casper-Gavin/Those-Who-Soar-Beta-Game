using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDetect : MonoBehaviour {
    public bool isInBossZone;

    private void Awake() {
        isInBossZone = false;
    }

    private void Update() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isInBossZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isInBossZone = false;
        }
    }
}

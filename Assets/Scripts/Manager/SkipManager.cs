using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipManager : MonoBehaviour {
    public float holdTime = 1.0f;
    public Image skipImage;

    private float timeHeld = 0.0f;
    private bool isHolding = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            OnHold();
        }

        if (isHolding) {
            timeHeld += Time.deltaTime;
            skipImage.fillAmount = timeHeld / holdTime;

            if (timeHeld >= holdTime) {
                DialogueManager.Instance.EndAllDialogue();
            }
        }

        if (Input.GetKeyUp(KeyCode.F)) {
            OnRelease();
        }
    }

    public void OnHold() {
        isHolding = true;
    }

    private void OnRelease() {
        isHolding = false;
        timeHeld = 0.0f;
        skipImage.fillAmount = 0.0f;
    }
}

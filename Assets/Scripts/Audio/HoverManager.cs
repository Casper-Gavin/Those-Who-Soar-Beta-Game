using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverManager : MonoBehaviour, IPointerEnterHandler {
    private AudioManager audioManager;

    private void Start() {
        audioManager = AudioManager.Instance;
    }

    private void Update() {
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //HoverVolume(0.05f);
        HoverSound();
        
        //StartCoroutine(HoverSoundExit());
    }

    public void OnPointerExit(PointerEventData eventData) {
        HoverSoundStop();
    }

    public void HoverSound() {

        if (audioManager != null) {
            audioManager.MakeAndPlaySFX("HoverButton");
        }
    }

    public void HoverSoundStop() {
        if (audioManager != null) {
            audioManager.StopSFX("HoverButton");
        }
    }

    public void HoverVolume(float volume) {
        if (audioManager != null) {
            audioManager.SetSFXVolume("HoverButton", volume);
        }
    }

    private IEnumerator HoverSoundExit() {
        yield return new WaitForSeconds(0.1f);
        HoverVolume(0);
    }
}

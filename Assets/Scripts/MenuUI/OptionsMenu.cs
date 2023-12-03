using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    private AudioManager audioManager;

    public Button fullScreenButton;
    private bool isFullscreen;

    private string currentlyPlaying;

    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();

        // Set the volume slider to the current volume
        currentlyPlaying = audioManager.GetCurrentlyPlaying();
        gameObject.GetComponentInChildren<UnityEngine.UI.Slider>().value = audioManager.GetVolume(currentlyPlaying);
    }

    public void SetVolume(float volume) {
        //Debug.Log(volume);

        currentlyPlaying = audioManager.GetCurrentlyPlaying();
        audioManager.SetVolume(currentlyPlaying, volume);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}

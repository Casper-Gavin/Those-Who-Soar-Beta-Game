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

    private void Update() {
        //isFullscreen = Screen.fullScreen;
        //fullScreenButton.GetComponentInChildren<Text>().text = isFullscreen ? "Fullscreen" : "Windowed";
    
        GameObject selfObject = GameObject.Find("OptionsMenu");
        if (Input.GetKeyDown(KeyCode.Escape) && selfObject) {
            gameObject.SetActive(false);

            GameObject.Find("PauseMenu").SetActive(true);
        }
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

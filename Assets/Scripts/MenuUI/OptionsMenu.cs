using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    private AudioManager audioManager;

    public Button fullScreenButton;

    private string currentlyPlaying;

    private bool cursorVisibilityToRestore;
    public void OnEnable()
    {
        cursorVisibilityToRestore = Cursor.visible;
        Cursor.visible = true;
    }

    public void OnDisable()
    {
        Cursor.visible = cursorVisibilityToRestore;
    }

    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();

        // Set the volume slider to the current volume
        currentlyPlaying = audioManager.GetCurrentlyPlaying();
        gameObject.GetComponentInChildren<UnityEngine.UI.Slider>().value = audioManager.GetVolume(currentlyPlaying);
    }

    private void Update() {    
        GameObject selfObject = GameObject.Find("OptionsMenu");
        if (Input.GetKeyDown(KeyCode.Escape) && selfObject) {
            gameObject.SetActive(false);

            GameObject.Find("PauseMenu").SetActive(true);
            Cursor.visible = true;
        }
    }

    public void SetVolume(float volume) {
        //Debug.Log(volume);

        currentlyPlaying = audioManager.GetCurrentlyPlaying();
        audioManager.SetVolume(currentlyPlaying, volume);
    }

    public void ToggleFullscreen() {
        Screen.fullScreen = !Screen.fullScreen;
    }
}

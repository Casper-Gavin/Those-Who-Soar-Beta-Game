using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;

    public Button fullScreenButton;

    private string currentlyPlaying;

    private bool cursorVisibilityToRestore;
    public void OnEnable()
    {
        cursorVisibilityToRestore = Cursor.visible;
        Cursor.visible = true;

        Debug.Log("Volume fetched onEnable");
        // Set the volume slider to the current volume
        currentlyPlaying = AudioManager.Instance.GetCurrentlyPlaying();
        gameObject.GetComponentInChildren<UnityEngine.UI.Slider>().value = AudioManager.Instance.GetVolume(currentlyPlaying);
    }

    public void OnDisable()
    {
        Cursor.visible = cursorVisibilityToRestore;
    }

    private void Start() {
        Debug.Log("Volume fetched start");

        // Set the volume slider to the current volume
        currentlyPlaying = AudioManager.Instance.GetCurrentlyPlaying();
        gameObject.GetComponentInChildren<UnityEngine.UI.Slider>().value = AudioManager.Instance.GetVolume(currentlyPlaying);
    }

    private void Update() {    
        GameObject selfObject = GameObject.Find("OptionsMenu");
        if (Input.GetKeyDown(KeyCode.Escape) && selfObject) {
            gameObject.SetActive(false);

            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                mainMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
            Cursor.visible = true;
        }
    }

    public void SetVolume(float volume) {
        currentlyPlaying = AudioManager.Instance.GetCurrentlyPlaying();
        Debug.Log("Options menu set volume for " + currentlyPlaying + " to " + volume);
        AudioManager.Instance.SetVolume(currentlyPlaying, volume);
    }

    public void ToggleFullscreen() {
        Screen.fullScreen = !Screen.fullScreen;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;

    public Button fullScreenButton;
    private bool cursorVisibilityToRestore;
    public void OnEnable()
    {
        cursorVisibilityToRestore = Cursor.visible;
        Cursor.visible = true;

        // Set the volume slider to the current volume
        gameObject.GetComponentInChildren<UnityEngine.UI.Slider>().value = AudioManager.Instance.GetVolume();
    }

    public void OnDisable()
    {
        Cursor.visible = cursorVisibilityToRestore;
    }

    private void Start() {

        // Set the volume slider to the current volume
        gameObject.GetComponentInChildren<UnityEngine.UI.Slider>().value = AudioManager.Instance.GetVolume();
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
        AudioManager.Instance.SetVolume(volume);
    }

    public void ToggleFullscreen() {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen) {
            Screen.SetResolution(1920, 1080, false);
        } else {
            // Set the screen resolution to the best resolution for the current monitor
            Resolution[] resolutions = Screen.resolutions;
            Resolution bestResolution = resolutions[resolutions.Length - 1];
            Screen.SetResolution(bestResolution.width, bestResolution.height, true);
        }
    }    

    public void ClickButton()
    {
        AudioManager.ClickButton();
    }

    public void ClickSkill()
    {
        AudioManager.ClickSkill();
    }
}

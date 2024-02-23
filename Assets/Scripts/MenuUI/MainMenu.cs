using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;

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

    public void PlayGame() {
        SceneManager.LoadScene("TestScene");
    }

    public void PlayTutorial() {
        SceneManager.LoadScene("TutorialScene");
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OptionsButtonPressed()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
        Cursor.visible = true;
    }
}

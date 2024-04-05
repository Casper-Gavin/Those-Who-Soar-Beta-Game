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

    // if you want to be able to go to a scene from main menu, you need to make sure it's in the build
    // in Unity (on the scene you want to add), file -> build settings -> add open scenes

    public void PlayGame() {
        SceneManager.LoadScene("LevelFiveScene"); 
    }

    public void PlayLevelOne() {
        SceneManager.LoadScene("LoreScene"); 
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

    public void ClickButton()
    {
        AudioManager.ClickButton();
    }

    public void ClickSkill()
    {
        AudioManager.ClickSkill();
    }
}

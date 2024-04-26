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

    public void SetCursorVisible()
    {
        Cursor.visible = true;
    }

    public void SetCursorInvisible()
    {
        Cursor.visible = false;
    }

    public void ResetPlayerPrefs()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (manager)
        {
            manager.ResetPlayerPrefs();
        }
        else
        {
            Debug.Log("ERROR! No Game Manager!!");
        }
    }

    // if you want to be able to go to a scene from main menu, you need to make sure it's in the build
    // in Unity (on the scene you want to add), file -> build settings -> add open scenes

    public void PlayGame() {
        SceneManager.LoadScene("TestScene"); 
    }

    public void Play() {
        // if the CurrentScene is set in the GameManager, load that scene
        if (GameManager.Instance.CURRENT_SCENE != "" && GameManager.Instance.CURRENT_SCENE != null) {
            PlayerPrefs.SetString("CURRENT_SCENE", GameManager.Instance.CURRENT_SCENE);
            SceneManager.LoadScene(PlayerPrefs.GetString("CURRENT_SCENE"));
        } else {
            PlayerPrefs.SetString("CURRENT_SCENE", PlayerPrefs.GetString("DEFAULT_SCENE"));
            SceneManager.LoadScene(PlayerPrefs.GetString("DEFAULT_SCENE"));
        }
    }

    public void PlayLoreScene() {
        SceneManager.LoadScene("LoreScene"); 
    }

    public void PlayLevelOne() {
        SceneManager.LoadScene("LevelOneScene");
    }

    public void PlayLevelTwo() {
        SceneManager.LoadScene("LevelTwoScene");
    }

    public void PlayLevelThree() {
        SceneManager.LoadScene("LevelThreeScene");
    }

    public void PlayLevelFour() {
        SceneManager.LoadScene("LevelFourScene");
    }

    public void PlayLevelFive() {
        SceneManager.LoadScene("LevelFiveScene");
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

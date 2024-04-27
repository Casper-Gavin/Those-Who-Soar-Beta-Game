using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : Singleton<DialogueManager> {
    private Queue<string> sentences;

    [Header("UI Elements")]
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI dialogueText;
    public TMPro.TextMeshProUGUI continueButton;
    public Animator animator;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private int frequencyLevel = 3;
    [Range(-3.0f, 3.0f)]
    [SerializeField] private float minPitch = 0.85f;
    [Range(-3.0f, 3.0f)]
    [SerializeField] private float maxPitch = 1.10f;
    [SerializeField] private bool isRandomSFX = false;
    [SerializeField] private bool isRandomNotPred = true;
    [SerializeField] private bool isPredictableSFX = true; // Make this opposite of isRandomSFX
    [SerializeField] private float volume = 0.65f;

    public GameObject[] imgs;
    public bool dialogueIsDisplaying = false;
    private int currentImg = 0;

    [SerializeField] private GameObject character;
    [SerializeField] public GameObject[] dialogueCanvas;

    [SerializeField] public GameObject dialogueCanvasObj;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject skillTreeMenu;
    [SerializeField] private GameObject inputMenu;
    [SerializeField] private GameObject[] vendorDialogues;

    private void Start() {
        sentences = new Queue<string>();

        if (audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if (SceneManager.GetActiveScene().name == "LoreScene") {
            for (int i = 0; i < imgs.Length; i++) {
                if (i == 0) {
                    imgs[i].SetActive(true);
                } else {
                    imgs[i].SetActive(false);
                }
            }

            StartDialogue(imgs[currentImg].GetComponent<DialogueTrigger>().dialogue);
        }

        if (character == null) {
            character = GameObject.Find("Player");
        }
    }

    private void Update() {
        if (audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if (character == null) {
            character = GameObject.Find("Player");
        }

        if (SceneManager.GetActiveScene().name == "LoreScene") {
            // Return is the same as Enter
            if (Input.GetKeyDown(KeyCode.Return) && !dialogueIsDisplaying) {
                currentImg++;
                if (currentImg == imgs.Length)
                {
                    SceneManager.LoadScene("LevelOneScene");
                    return;
                }
                StartDialogue(imgs[currentImg].GetComponent<DialogueTrigger>().dialogue);
                HandleImages(currentImg);
            } else if (Input.anyKeyDown && dialogueIsDisplaying) {
                DisplayNextSentence();
            } else if (Input.GetKeyDown(KeyCode.Escape) && dialogueIsDisplaying) {
                EndDialogue();
            }

            if (currentImg == imgs.Length) {
                SceneManager.LoadScene("LevelOneScene");
            }
        } else if (SceneManager.GetActiveScene().name == "TutorialScene") {
            if (!dialogueIsDisplaying) {
                // if the character is in any of the dialogueCanvas's transform, then start the dialogue
                for (int i = 0; i < dialogueCanvas.Length; i++) {
                    if (dialogueCanvas[i].GetComponent<DialogueTrigger>().isInDialogue) {
                        StartDialogue(dialogueCanvas[i].GetComponent<DialogueTrigger>().dialogue);
                    }
                }
            } else if (Input.GetKeyDown(KeyCode.Q) && dialogueIsDisplaying) {
                DisplayNextSentence();
            } else if (dialogueIsDisplaying) {
                int counter = 0;
                for (int i = 0; i < dialogueCanvas.Length; i++) {
                    if (!dialogueCanvas[i].GetComponent<DialogueTrigger>().isInDialogue) {
                        counter++;
                    }
                }

                if (counter == dialogueCanvas.Length) {
                    EndDialogue();
                    CloseDialogue();
                }
            }
        } 

        SetContinueButton();

        // if the scene is not the main menu, lore scene, tutorial scene, or test scene, then check if the game is paused
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "LoreScene" && SceneManager.GetActiveScene().name != "TutorialScene" && SceneManager.GetActiveScene().name != "TestScene") {
            //CheckIsPaused();
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("IsOpen", true); // Open the dialogue box
        
        nameText.text = dialogue.name;

        sentences.Clear(); // Clear the queue from previous sentences

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence); // Add the sentences to the queue
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueIsDisplaying = true;
        StopAllCoroutines(); // Stop the previous sentence from being typed
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        yield return new WaitForSeconds(0.35f);
        int counter = 0;
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;

            if ((counter % frequencyLevel == 0) && audioManager != null) {
                if (!isPredictableSFX){
                    float randomPitch = Random.Range(minPitch, maxPitch);

                    if (isRandomNotPred) {
                        audioManager.MakeAndPlaySFXVariable("TextSound", randomPitch, volume);
                    } else {
                        int randomIndex = Random.Range(0, 2);

                        if (randomIndex == 0) {
                            audioManager.MakeAndPlaySFXVariable("TextSound", randomPitch, volume);
                        } else {
                            audioManager.MakeAndPlaySFXVariable("TextSound1", randomPitch, volume);
                        }
                    }
                } else {
                    int currentCharHash = letter.GetHashCode();
                    int predIndex = currentCharHash % 2;

                    int minPitchInt = (int)(minPitch * 100);
                    int maxPitchInt = (int)(maxPitch * 100);
                    int pitchRange = maxPitchInt - minPitchInt;

                    int randomRange = Random.Range(0, 3);

                    if (pitchRange != 0) {
                        int predPitchInt = minPitchInt + (currentCharHash % pitchRange);
                        float predPitch = predPitchInt / 100.0f;

                        if (randomRange == 0) {
                            audioManager.MakeAndPlaySFXVariable("TextSound", predPitch, volume);
                        } else if (randomRange == 1) {
                            audioManager.MakeAndPlaySFXVariable("TextSound3", predPitch, volume);
                        } else {
                            audioManager.MakeAndPlaySFXVariable("TextSound4", predPitch, volume);
                        }
                    } else {
                        audioManager.MakeAndPlaySFXVariable("TextSound", minPitch, volume);
                    }
                }
            }

            counter++;

            yield return new WaitForSeconds(0.03f);
            // yield return null;
        }
    }

    private void SetContinueButton() {
        if (sentences.Count == 0) {
            dialogueIsDisplaying = false;

            if (SceneManager.GetActiveScene().name == "LoreScene") {
                if (currentImg == imgs.Length - 1) {
                    continueButton.text = "End >>>";
                } else {
                    continueButton.text = "Next >>>";
                }
            } else {
                continueButton.text = "End >>>";
            }
        } else {
            dialogueIsDisplaying = true;

            continueButton.text = "Continue >>>";
        }
    }

    public void EndDialogue() {
        animator.SetBool("IsOpen", false); // Close the dialogue box
    }

    public void EndAllDialogue() {
        animator.SetBool("IsOpen", false); // Close the dialogue box
        sentences.Clear();

        SceneManager.LoadScene("LevelOneScene");
    }

    public void CloseDialogue() {
        animator.SetBool("IsOpen", false); // Close the dialogue box
        sentences.Clear();
    }

    private void HandleImages(int counter) {
        if (counter < imgs.Length) {
            imgs[counter].SetActive(true);
            
            for (int i = 0; i < imgs.Length; i++) {
                if (i != counter) {
                    imgs[i].SetActive(false);
                }
            }
        }
    }

    private void CheckIsPaused() {
        if (pauseMenu.activeSelf || optionsMenu.activeSelf || skillTreeMenu.activeSelf || inputMenu.activeSelf) {
            if (dialogueIsDisplaying && dialogueCanvasObj.activeSelf) {
                Time.timeScale = 1f;
                dialogueCanvasObj.SetActive(false);
                animator.SetBool("IsOpen", false);
                Time.timeScale = 0f;
            }
        } else {
            if (dialogueIsDisplaying && !dialogueCanvasObj.activeSelf) {
                dialogueCanvasObj.SetActive(true);
                animator.SetBool("IsOpen", true);
            }
        }
    }
}

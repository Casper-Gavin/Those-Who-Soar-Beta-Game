using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager> {
    private Queue<string> sentences;

    [Header("UI Elements")]
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI dialogueText;
    public TMPro.TextMeshProUGUI continueButton;
    public Animator animator;

    private void Start() {
        sentences = new Queue<string>();
    }

    private void Update() {
        SetContinueButton();
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
        StopAllCoroutines(); // Stop the previous sentence from being typed
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        yield return new WaitForSeconds(0.35f);
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
            // yield return null;
        }
    }

    private void SetContinueButton() {
        if (sentences.Count == 0) {
            continueButton.text = "End >>>";
        } else {
            continueButton.text = "Continue >>>";
        }
    }

    public void EndDialogue() {
        animator.SetBool("IsOpen", false); // Close the dialogue box
    }
}

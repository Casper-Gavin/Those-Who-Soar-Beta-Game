using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;

    public void TriggerDialogue() {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void EndDialogue() {
        DialogueManager.Instance.EndDialogue();
    }

    public void EndAllDialogue() {
        DialogueManager.Instance.EndAllDialogue();
    }

    public void SetName(string name) {
        dialogue.name = name;
    }

    public void SetSentences(string[] sentences) {
        dialogue.sentences = sentences;
    }
}

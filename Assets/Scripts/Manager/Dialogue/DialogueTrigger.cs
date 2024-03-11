using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;

    public void TriggerDialogue() {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void EndDialogue() {
        DialogueManager.Instance.EndDialogue();
    }

    public void SetName(string name) {
        dialogue.name = name;
    }

    public void SetSentences(string[] sentences) {
        dialogue.sentences = sentences;
    }
}

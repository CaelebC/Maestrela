using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    DialogueManager dialogueManager;

    private void Start() {
        dialogueManager = DialogueManager.instance;
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}

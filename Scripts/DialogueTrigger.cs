using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Canvas dialogCanvas;
    private void Start()
    {
        dialogCanvas.enabled = false;
      
    }

    public void TriggerDialogue()
    {
        dialogCanvas.enabled = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

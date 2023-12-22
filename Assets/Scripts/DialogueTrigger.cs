using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueBox;
    public GameObject dialogueButton;
    public playerMovement playerMovement;

    public void TriggerDialogue()
    {
        playerMovement.enabled = false;
        dialogueButton.SetActive(false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        dialogueBox.SetActive(true); 
    }
}

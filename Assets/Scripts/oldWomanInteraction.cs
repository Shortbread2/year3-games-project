using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWomanInteraction : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private PlayerNPCInteraction npcInteraction;
    public GameObject collisionWithWoman;

    private void Start()
    {
        npcInteraction = GetComponent<PlayerNPCInteraction>();
        if (npcInteraction == null)
        {
            Debug.LogError("PlayerNPCInteraction component null");
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager null");
        }
    }

    private void Update()
    {
        if (dialogueManager != null && dialogueManager.isDialogueEnded)
        {
            npcInteraction.startTimer();
            dialogueManager.isDialogueEnded = false;
            collisionWithWoman.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText; 
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    private Queue<string> sentences;
    public bool isDialogueEnded = false;

    public playerMovement playerMovement;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueEnded = false;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }


    public void EndDialogue()
    {
        Debug.Log("End of conversation");
        dialogueBox.SetActive(false);
        isDialogueEnded = true;
        playerMovement.enabled = true;
    }
}

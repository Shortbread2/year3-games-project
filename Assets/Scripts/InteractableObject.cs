using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject puzzleButton;
    public bool isScriptActive = true;

    private void Start()
    {
        puzzleButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (isScriptActive) {
            if (player.gameObject.CompareTag("Player"))
            {
                puzzleButton.SetActive(true);
            }
        }
        else
        {
            puzzleButton.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (isScriptActive) { 
            if (player.gameObject.CompareTag("Player"))
            {
                puzzleButton.SetActive(false);
            }
        } 
        else
        {
            puzzleButton.SetActive(false);
        }
    } 
}



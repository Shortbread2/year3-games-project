using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject interactButton;
    public bool isScriptActive = true;

    private void Start()
    {
        interactButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (isScriptActive) {
            if (player.gameObject.CompareTag("Player"))
            {
                Debug.Log("Trigger Detected with Player");
                interactButton.SetActive(true);
            }
        }
        else
        {
            interactButton.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (isScriptActive) { 
            if (player.gameObject.CompareTag("Player"))
            {
                interactButton.SetActive(false);
            }
        } 
        else
        {
            interactButton.SetActive(false);
        }
    } 
}



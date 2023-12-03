using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    //this script is attached to the InteractableObject in the scene
    //used to show interaction (later used for puzzles)

    public GameObject interactButton; //reference to interact button
    public GameObject puzzlePanel; //Puzzle panel (will be replaced with actual puzzle scene later using sceneManagement
    public GameObject emptyBackground; //used for when the player clicks outside the puzzle panel (can be replaced with a exit button later)

    void Start()
    {
        //Hide the interact button and puzzle panel at the start
        HideInteractButton();
        HidePuzzlePanel();
    }

    public void ShowInteractButton()
    {
        //Display interact button
        //called within PlayerInteraction
        interactButton.SetActive(true);
    }

    public void HideInteractButton()
    {
        //Hide interact button
        //Called within PlayerInteraction
        interactButton.SetActive(false);
    }

    public void OnInteractButtonClick()
    {
        //Show puzzle panel and hide interact button
        //Called within PlayerInteraction
        Debug.Log("Button clicked");
        ShowPuzzlePanel();
        HideInteractButton();
    }

    public void HidePuzzlePanel()
    {
        //Hide puzzle panel and empty background
        puzzlePanel.SetActive(false);
        emptyBackground.SetActive(false);
    }

    void ShowPuzzlePanel()
    {
        //Show puzzle panel and empty background
        puzzlePanel.SetActive(true);
        emptyBackground.SetActive(true);
    }

    public void OnBackgroundClick()
    {
        //Check if the player is still colliding with the interactable object
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        bool isPlayerColliding = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                isPlayerColliding = true;
                break;
            }
        }

        //If the player is still colliding, show the interact button; otherwise, hide it
        if (isPlayerColliding)
        {
            ShowInteractButton();
        }
        else
        {
            HideInteractButton();
        }

        //Close the puzzle panel
        HidePuzzlePanel();

        //Hide the transparent background
        emptyBackground.SetActive(false);
    }

}

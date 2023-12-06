using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    public void OpenInventoryScene()
    {
        Debug.Log("Opening Inventory Scene...");
        SceneManager.LoadScene("Inventory"); 
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenPauseMenu()
    {
        SceneManager.LoadScene("Pause Menu");
    }

    public void OpenPuzzle1()
    {
        SceneManager.LoadScene("Mining Level");
    }

    // Add more functions for other button actions as needed
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    public void OpenNewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenPuzzle1()
    {
        SceneManager.LoadScene("Mining Level");
    }

    public void OpenInventoryScene()
    {
        Debug.Log("Opening Inventory Scene...");
        SceneManager.LoadScene("Inventory");
    }

    // Add more functions for other button actions as needed
}

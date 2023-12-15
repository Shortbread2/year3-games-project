using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDisplayManager : MonoBehaviour
{
    public void OpenNewGame()
    {
        SceneManager.LoadScene("SampleScene-LayansChangesWithInv");
    }

    public void OpenPuzzle1()
    {
        SceneManager.LoadScene("Mining Level");
    }

    // Add more functions for other button actions as needed
}

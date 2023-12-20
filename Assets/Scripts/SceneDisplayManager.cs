using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDisplayManager : MonoBehaviour
{
    public void OpenNewGame()
    {
        SceneManager.LoadScene("Mining Level 1");
    }

    public void OpenLockpickingPuzzle()
    {

        SceneManager.LoadScene("Lockpicking puzzle scene");
    }

    public void OpenRaceChase()
    {
        SceneManager.LoadScene("Race Chase");
    }

    public void OpenLevel2()
    {
        SceneManager.LoadScene("Lab Level 2");
    }

    // Add more functions for other button actions as needed
}

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

    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Add more functions for other button actions as needed
}

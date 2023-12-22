using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject MainUIPanel;
    public void Pause()
    {
        MainUIPanel.GetComponent<Canvas>().sortingOrder = 6;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        MainUIPanel.GetComponent<Canvas>().sortingOrder = 0;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
}

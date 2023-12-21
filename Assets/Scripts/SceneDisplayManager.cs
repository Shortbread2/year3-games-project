using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDisplayManager : MonoBehaviour
{
    public void OpenNewGame()
    {
        LoadSceneByIndex(1);
    }

    public void OpenNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        LoadSceneByIndex(currentIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadSceneByIndex(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogWarning("Invalid scene index");
        }
    }
}

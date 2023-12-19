using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPuzzleScene : MonoBehaviour
{
    public string scene;

    public void OpenSceneWithDelay()
    {
        // Invoke the OpenScene method after a delay of 4 seconds
        Invoke("OpenScene", 4f);
    }

    private void OpenScene()
    {
        // Load the specified scene
        SceneManager.LoadScene(scene);
    }
}

//Ismail Hendryx
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPuzzleScene : MonoBehaviour
{
    

    public void OpenSceneWithDelay()
    {
        // Invoke the OpenScene method after a delay of 2 seconds
        Invoke("OpenScene", 2f);
    }

    private void OpenScene()
    {
        // Load the specified scene
        SceneManager.LoadScene("");
    }
}

//Ismail Hendryx
using System.Collections;
using UnityEngine;

public class CutScenes : MonoBehaviour
{
    void Start()
    {
        // Freeze time
        Time.timeScale = 0;

    }

    public void unfreezeTime()
    {
        Time.timeScale = 1;
    }
}

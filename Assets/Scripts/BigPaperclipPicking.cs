using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPaperclipPicking : MonoBehaviour
{
    public GameObject SubsequentPaperclip;
    public GameObject CurrentLock;
    public GameObject SubsequentLock;

    void Update()
    {
        // Check if the B key is clicked
        if (Input.GetKeyDown(KeyCode.B))
        {
            Bendpaperclip();
        }
    }

    void Bendpaperclip()
    {
        // Store the last position of the catching hand

        Destroy(gameObject);
        Destroy(CurrentLock);
        SubsequentLock.SetActive(true);
        SubsequentPaperclip.SetActive(true);
    }
}

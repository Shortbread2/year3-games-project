using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPaperclipPicking : MonoBehaviour
{
    private float bKeyTimer = 0f;
    private bool isBKeyDown = false;
    public GameObject SubsequentPaperclip;
    public GameObject CurrentLock;
    public GameObject SubsequentLock;

    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            if (!isBKeyDown)
            {
                // B key was just pressed
                isBKeyDown = true;
                bKeyTimer = 0f;
            }
            else
            {
                // B key is being held
                bKeyTimer += Time.deltaTime;

                // Check if B is held for 3 seconds
                if (bKeyTimer >= 3f)
                {
                    Bendpaperclip(); // Call your function or perform your action here
                    isBKeyDown = false; // Reset the flag
                }
            }
        }
        else
        {
            // B key is not pressed, reset the timer and flag
            isBKeyDown = false;
            bKeyTimer = 0f;
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

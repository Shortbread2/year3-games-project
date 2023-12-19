using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperclipbending : MonoBehaviour
{
    public GameObject BentpaperclipHand;
    private Vector3 lastHandPos;
    private Quaternion lastHandRotation;

    void Update()
    {
        // Check if the B key is clicked
        if (Input.GetKeyDown(KeyCode.B))
        {
            Bendpaperclip(); // Call your function or perform your action here
        }
    }

    void Bendpaperclip()
    {
        // Store the last position of the catching hand
        lastHandPos = gameObject.transform.position;
        lastHandRotation = gameObject.transform.rotation;

        Destroy(gameObject);
        BentpaperclipHand.SetActive(true);
        BentpaperclipHand.transform.position = lastHandPos;
        BentpaperclipHand.transform.rotation = lastHandRotation;
    }
}

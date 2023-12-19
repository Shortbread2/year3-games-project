using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanEnters : MonoBehaviour
{
    public playerMovement PlayerMove;
    public float maxYPosition = -2.9f; // Adjust the maximum Y position
    public GameObject endgame;

    private bool hasReachedMaxPosition = false;

    void Update()
    {
        // Check if the script is not active and Y position is below the threshold
        if (!PlayerMove.isScriptActive && transform.position.y < maxYPosition)
        {
            // Move the object upward (along the Y-axis)
            transform.Translate(Vector3.up * 3f * Time.deltaTime);
        }
        else
        {
            // If the object has reached the maxYPosition, activate endgame after a delay
            if (!hasReachedMaxPosition && transform.position.y >= maxYPosition)
            {
                Invoke("ActivateEndgame", 2f); // Invoke the method after a delay of 2 seconds
                hasReachedMaxPosition = true; // Set the flag to true to prevent repeated activation
            }
        }
    }

    // Method to activate endgame
    void ActivateEndgame()
    {
        endgame.SetActive(true);
    }
}

//Ismail Hendryx
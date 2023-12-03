using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    //this script is attached to the main camera in the scene
    //Ensures the player object is always in the middle of the screen (follows)

    public Transform player; //reference to player's transform
    public float smoothSpeed = 2f; //adjust value to control the smoothness of camera movement

    void LateUpdate()
    {
        if (player != null)
        {
            //target position for the camera based on player's position
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // use lerp to smoothly move the camera towards the targetPosition
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}

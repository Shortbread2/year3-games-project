using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //this script is attached to the player object in the scene
    //used for detecting collision with other objects 

    //Called when player collides with another 2D Collider
    void OnCollisionEnter2D(Collision2D other)
    {
        //check tag 'interactable' (attached to interactable objects)
        if (other.collider.tag == "Interactable")
        {
            //call ShowInteractButton method from InteractableObject.cs of the other object
            other.collider.GetComponent<InteractableObject>().ShowInteractButton();

            //debug to test collision detected with Interactable object
            Debug.Log("Collision detected with interactable object");
        }
    }

    //Called when player stops colliding with another 2D collider
    void OnCollisionExit2D(Collision2D other)
    {
        //check tag 'interactable'
        if (other.collider.tag == "Interactable")
        {
            // call HideInteractButton method from InteractableObject.cs of the other object 
            other.collider.GetComponent<InteractableObject>().HideInteractButton();

            //debug to test collision exit with Interactable object
            Debug.Log("Collision exit with interactable object");
        }
    }
}

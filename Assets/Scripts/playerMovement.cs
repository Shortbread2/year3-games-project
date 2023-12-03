using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //this script is attached to the player object in the scene
    //basic controls of moving up, down, left and right

    public float moveSpeed = 1f; //player movement speed
    public Rigidbody2D rb; //reference to Rigidbody2D component 
    public Animator animator; //reference to Animator component
    Vector2 movement; //store movement input

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //get input for horizontal movement axis
        movement.y = Input.GetAxisRaw("Vertical"); //get input for vertial movement axis

        //Set animator parameters based on input
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        //move player object position
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}

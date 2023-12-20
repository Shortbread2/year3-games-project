using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //this script is attached to the player object in the scene
    //basic controls of moving up, down, left and right
    public float moveSpeed = 1f; // player movement speed
    public float runSpeed = 2f; // player run speed
    public Rigidbody2D rb; // reference to Rigidbody2D component
    public Animator animator; // reference to Animator component
    public bool isScriptActive;
    Vector2 movement; //store movement input

    void Update()

    {
        movement.x = Input.GetAxisRaw("Horizontal"); //get input for horizontal movement axis
        movement.y = Input.GetAxisRaw("Vertical"); //get input for vertial movement axis
        if (isScriptActive)
        {
            movement.x = Input.GetAxisRaw("Horizontal"); // get input for horizontal movement axis
            movement.y = Input.GetAxisRaw("Vertical"); // get input for vertical movement axis

        movement.Normalize();
            movement.Normalize();

        //Set animator parameters based on input
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.magnitude);
            // Set animator parameters based on input
            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetFloat("speed", movement.magnitude);
        }
    }

    void FixedUpdate()
    {
        //move player object position
        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);
        if (isScriptActive)
        {
            // Move player object position
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                rb.AddForce(movement * runSpeed * Time.fixedDeltaTime);
            }
            else
            {
                rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

}
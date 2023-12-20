using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //this script is attached to the player object in the scene
    //basic controls of moving up, down, left and right

    public float moveSpeed = 1f; //player movement speed
    public float sprintMultiplier = 1.4f;
    private float sprintModifier = 1f;
    public Rigidbody2D rb; //reference to Rigidbody2D component 
    public Animator animator; //reference to Animator component
    Vector2 movement; //store movement input

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //get input for horizontal movement axis
        movement.y = Input.GetAxisRaw("Vertical"); //get input for vertial movement axis

        movement.Normalize();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            sprintModifier = sprintMultiplier;
        }
        else
        {
            sprintModifier = 1f;
        }

        //Set animator parameters based on input
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.magnitude);
        animator.SetFloat("MoveSpeed", sprintModifier);

    }

    private void FixedUpdate()
    {
        //move player object position
        rb.AddForce(movement * moveSpeed * sprintModifier * Time.fixedDeltaTime);
    }

}

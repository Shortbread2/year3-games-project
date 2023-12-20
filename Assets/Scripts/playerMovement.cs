using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // player movement speed
    public float runSpeed = 2f; // player run speed
    public Rigidbody2D rb; // reference to Rigidbody2D component
    public Animator animator; // reference to Animator component
    public bool isScriptActive;

<<<<<<< Updated upstream
    Vector2 movement; // store movement input
=======
    public float moveSpeed = 1f; //player movement speed
    public float sprintMultiplier = 1.4f;
    private float sprintModifier = 1f;
    public Rigidbody2D rb; //reference to Rigidbody2D component 
    public Animator animator; //reference to Animator component
    Vector2 movement; //store movement input
>>>>>>> Stashed changes

    void Update()

    {
        if (isScriptActive)
        {
            movement.x = Input.GetAxisRaw("Horizontal"); // get input for horizontal movement axis
            movement.y = Input.GetAxisRaw("Vertical"); // get input for vertical movement axis

            movement.Normalize();

<<<<<<< Updated upstream
            // Set animator parameters based on input
            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetFloat("speed", movement.magnitude);
        }
=======
        if (Input.GetKey(KeyCode.LeftShift)){
            sprintModifier = sprintMultiplier;
        } else{
             sprintModifier = 1f;
        }

        //Set animator parameters based on input
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.magnitude);
        animator.SetFloat("MoveSpeed", sprintModifier);

>>>>>>> Stashed changes
    }

    void FixedUpdate()
    {
<<<<<<< Updated upstream
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
=======
        //move player object position
        rb.AddForce(movement * moveSpeed * sprintModifier * Time.fixedDeltaTime);
>>>>>>> Stashed changes
    }
}

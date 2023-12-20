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

    Vector2 movement; // store movement input

    void Update()

    {
        if (isScriptActive)
        {
            movement.x = Input.GetAxisRaw("Horizontal"); // get input for horizontal movement axis
            movement.y = Input.GetAxisRaw("Vertical"); // get input for vertical movement axis

            movement.Normalize();

            // Set animator parameters based on input
            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetFloat("speed", movement.magnitude);
        }
    }

    void FixedUpdate()
    {
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
using System.Collections;
using UnityEngine;

public class carMovement : MonoBehaviour
{
    private float speed = 4f;
    private float upSpeed = 3f;
    private float minX = 75f;  // Set the minimum x-position
    private float maxX = 75.7f;  // Set the maximum x-position
    private float timer = 0f;
    private float maxTime = 60f; // Set the maximum time in seconds

    public Rigidbody2D rb;

    void Update()
    {
        // Check if we have reached 90 seconds
        if (timer >= maxTime)
        {
            // Slow down the object to a halt
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 0.5f); // Adjust the multiplier for the speed of slowdown

            return; // Optional: You can add code here to handle the event
        }

        // Update the timer
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.A) && transform.position.x > minX)
        {
            // Move the object to the left
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && transform.position.x < maxX)
        {
            // Move the object to the right
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
    }


}

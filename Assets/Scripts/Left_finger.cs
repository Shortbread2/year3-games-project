using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFinger : MonoBehaviour
{
    public float speed = 1000f; // Adjust the speed as needed
    public float rotationSpeed = 400f;

    void Update()
    {

        // Check if the 'A' key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            // Move the object to the left
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // Move the object to the right
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            // Move the object to the left
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            // Move the object to the right
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        // Check if the right arrow key is pressed
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Rotate the object to the right
            Rotate(-rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotate the object to the right
            Rotate(rotationSpeed * Time.deltaTime);
        }

    }

    void Rotate(float angle)
    {
        // Rotate the object
        transform.Rotate(Vector3.forward * angle);
    }



}


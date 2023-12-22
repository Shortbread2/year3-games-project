using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFinger : MonoBehaviour
{
    private float speed = 1000f; // Adjust the speed as needed
    private float rotationSpeed = 400f;

    public GameObject GameOverPanel;

    void Update()
    {
        Debug.Log(gameObject);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("exitedGame"))
        {
            Destroy(gameObject);
            Debug.Log("OPEN PAUSE MENU");
            GameOverPanel.SetActive(true);

            //TODO open pause menu to say game over if the other two hands fail
        }
        else { Debug.Log("um"); }
    }

}


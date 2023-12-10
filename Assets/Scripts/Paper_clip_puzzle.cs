using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper_clip_puzzle : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private RectTransform rectTransform;
    public GameObject paperclipHand;
    private Vector3 lastHandPos;
    private Quaternion lastHandRotation;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnableRigidbody();
        }
    }



    void Start()
    {
        DisableRigidbody();
        EnableRectTransform();
    }






    void DisableRigidbody()
    {
        // Disable the Rigidbody2D component if it exists
        if (rb2d != null)
        {
            // Disable the Rigidbody2D component
            rb2d.bodyType = RigidbodyType2D.Static;

        }
    }







    void EnableRigidbody()
    {
        if (rb2d == null)
        {
            // Add Rigidbody2D component if not already attached
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        }

        // Set gravity to *70 because it is suitable for this scale
        rb2d.gravityScale = 70f;
        rb2d.mass = 1f;


        //PhysicsMaterial2D bouncyMaterial = new PhysicsMaterial2D();
        //bouncyMaterial.bounciness = 0.3f; // paperclip bounciness
        //rb2d.sharedMaterial = bouncyMaterial;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }








    void EnableRectTransform()
    {
        // Ensure that the GameObject has a RectTransform component
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            // Set the position of the paperclip to a random position within the screen
            float randX = Random.Range(-600f, 600f);
            float randDeg = Random.Range(0f, 360f);

            Vector2 newPosition = new Vector2(randX, 800f); // Replace with the desired Y position
            Quaternion newRotation = Quaternion.Euler(0f, 0f, randDeg); // Replace with the desired rotation
            rectTransform.anchoredPosition = newPosition;
            rectTransform.localRotation = newRotation;
        }
    }







    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves another GameObject with a tag "CatchingHand"
        if (collision.gameObject.CompareTag("Catching Hand") || collision.gameObject.CompareTag("Bent Paperclip 1"))
        {
            // Store the last position of the catching hand
            lastHandPos = collision.gameObject.transform.position;
            lastHandRotation = collision.gameObject.transform.rotation;

            // Destroy the current object (this)
            Destroy(gameObject);

            // Destroy the catching hand object
            Destroy(collision.gameObject);

            // Activate the paperclip hand object and set its position
            paperclipHand.SetActive(true);
            paperclipHand.transform.position = lastHandPos;
            //paperclipHand.transform.rotation = lastHandRotation;
        }
    }


}

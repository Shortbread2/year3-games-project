using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the colliding object has the "projectile" tag
        if (other.gameObject.CompareTag("projectile"))
        {
            // Destroy the projectile object
            Destroy(other.gameObject);
        }
    }

}

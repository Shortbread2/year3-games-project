using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectileFromVan : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is BoxCollider2D && collision.gameObject.CompareTag("projectile"))
        {
            // Collision with a box collider and projectile
            Destroy(collision.gameObject); // Destroy the projectile
        }
    }
}

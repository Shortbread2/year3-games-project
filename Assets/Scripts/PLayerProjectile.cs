using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Animator animator;
    public float damage = 10f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = new Vector3(0, 0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        if (collision.gameObject.tag == "projectile"){
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (animator != null){
            // there is a script in the animator to destroy objects on a certain animation ending
            animator.SetTrigger("hitCollider");
        } else {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemy>().TakeDamage(damage);
        }
        if (collision.gameObject.tag == "NPC")
        {
            collision.gameObject.GetComponent<NPC>().TakeDamage(damage, GameObject.FindGameObjectWithTag("Player"));
        }

    }
}

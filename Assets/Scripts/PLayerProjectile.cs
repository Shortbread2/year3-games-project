using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Animator animator;
    public float damage = 10f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator != null){
            // there is a script in the animator to destroy objects on a certain animation ending
            animator.SetTrigger("hitCollider");
        } else {
            Destroy(gameObject, 0.25f);
        }
        // to get rid of physics on hit
        GetComponent<Rigidbody2D>().isKinematic = true;

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

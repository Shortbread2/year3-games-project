using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerProjectile : MonoBehaviour
{
    public GameObject hitEffect;

    public float damage = 10f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.25f);

        // dont know why it works but yea (shouldnt this just destroy itself before reading the if statement?)
        Destroy(gameObject);

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

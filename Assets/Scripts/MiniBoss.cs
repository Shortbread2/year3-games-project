using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public float health = 100f;
    private float currenthealth;
    public float displayhealth;
    public GameObject key;
    public Transform NameOFMiniBoss;
    public float Damage = 5f;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = health;
        displayhealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthealth <= 0)
        {
            GameObject Key = Instantiate(key, NameOFMiniBoss.position, NameOFMiniBoss.rotation);
            Destroy(Key, 60f);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        currenthealth -= damage;
        displayhealth = currenthealth;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(Damage);
        }
    }
}

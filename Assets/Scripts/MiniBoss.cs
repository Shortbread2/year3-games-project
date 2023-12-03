using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public int health = 100;
    private int currenthealth;
    public int displayhealth;
    public GameObject key;
    public Transform NameOFMiniBoss;
    public int Damage = 5;
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
    public void TakeDamage(int damage)
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

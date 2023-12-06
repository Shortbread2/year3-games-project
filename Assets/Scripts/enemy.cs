using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 100;
    private int currenthealth;
    public int displayhealth;
    public GameObject deathanimation;
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
            GameObject deathEffect = Instantiate(deathanimation, transform.position, Quaternion.identity);
            Destroy(deathEffect, 0.25f);
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        displayhealth = currenthealth;
    }
}

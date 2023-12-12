using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleHealth : MonoBehaviour
{
    public int health = 100;
    private int currenthealth;
    public int displayhealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = health;
        displayhealth = health;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            currenthealth -= 6;
            displayhealth = currenthealth;
            healthBar.SetHealth(currenthealth);
        }
    }
}

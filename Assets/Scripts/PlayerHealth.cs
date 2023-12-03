using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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
    public void PlayerTakeDamage(int Damage)
    {
        currenthealth -= Damage;
        displayhealth = currenthealth;
        healthBar.SetHealth(currenthealth);
    }
}

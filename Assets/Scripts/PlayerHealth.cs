using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    private float currenthealth;
    public float displayhealth;
    public HealthBar healthBar;
    public GameObject GameOverPanel;
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
            GameOverPanel.SetActive(true);
        }
    }
    public void PlayerTakeDamage(float Damage)
    {
        currenthealth -= Damage;
        displayhealth = currenthealth;
        healthBar.SetHealth(currenthealth);
    }
}

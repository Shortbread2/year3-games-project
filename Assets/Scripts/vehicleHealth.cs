using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleHealth : MonoBehaviour
{
    public int health = 100;
    private int currenthealth;
    public int displayhealth;
    public HealthBar healthBar;
    public GameObject GameOverPanel;
    public GameObject StopTimer;
    public GameObject RemoveKeypadScreen;

    public GameObject fadingPanel;

    public GameObject Van;

    public GameObject RemoveInstructions;

    public GameObject CanvasUI;
    void Start()
    {
        currenthealth = health;
        displayhealth = health;
        healthBar.SetMaxHealth(health);
        GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthealth <= 0 || gameObject == null)
        {
            GameOverPanel.SetActive(true);
            Destroy(gameObject);
            StopTimer.SetActive(false);
            RemoveKeypadScreen.SetActive(false);
            RemoveInstructions.SetActive(false);
            fadingPanel.SetActive(false);
            Van.SetActive(false);



        }
        if (currenthealth == 35)
        {
            GetComponent<ParticleSystem>().Play();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is CircleCollider2D && collision.gameObject.CompareTag("projectile"))
        {
            currenthealth -= 5;
            displayhealth = currenthealth;
            healthBar.SetHealth(currenthealth);
        }
    }
}

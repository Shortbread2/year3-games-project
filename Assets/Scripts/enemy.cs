using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health = 100;
    private int currenthealth;
    public int displayhealth;
    public int Damage = 5;
    public HealthBar healthBar;
    private Animator animator;
    public Behaviour aiPathfinder;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = health;
        displayhealth = health;
        healthBar.SetMaxHealth(health);
        animator = this.GetComponent<Animator>();
        animator.SetInteger("Health", health);
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthealth <= 0)
        {
            // death animation
            animator.SetTrigger("isDead");
            aiPathfinder.enabled = false;
            this.GetComponent<enemyBehaviour>().enabled = false;
            //this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().mass = 70f;

            //Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        displayhealth = currenthealth;
        healthBar.SetHealth(currenthealth);
        animator.SetTrigger("Hurt");
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float health = 100;
    private float currenthealth;
    public float displayhealth;
    public HealthBar healthBar;
    private Animator animator;
    public float damage;
    public AIBase aiPathfinder;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = health;
        displayhealth = health;
        healthBar.SetMaxHealth(health);
        animator = this.GetComponent<Animator>();
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
            this.GetComponent<EnemyMelee>().enabled = false;
            //this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().mass = 70f;

            //Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        currenthealth -= damage;
        displayhealth = currenthealth;
        healthBar.SetHealth(currenthealth);
        animator.SetTrigger("Hurt");
    }
}

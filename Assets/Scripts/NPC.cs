using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class NPC : EntitySuperScript
{
    private float currenthealth;
    public HealthBar healthBar;
    private Animator animator;
    public float damage;
    public AIBase aiPathfinder;
    private AIBehaviour aiBehaviour;
    private float turnToEnemyThreshold;
    // Start is called before the first frame update
    void Start()
    {
        aiBehaviour = this.GetComponent<AIBehaviour>();
        aiBehaviour.enabled = false;
        currenthealth = health;
        displayhealth = health;
        turnToEnemyThreshold = health * 0.6f;
        healthBar.SetMaxHealth(health);
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage, GameObject attacker)
    {
        aiBehaviour.enabled = true;
        currenthealth -= damage;
        displayhealth = currenthealth;
        healthBar.SetHealth(currenthealth);

        if (currenthealth <= 0)
        {
            // death animation
            animator.SetTrigger("isDead");
            aiPathfinder.enabled = false;
            this.GetComponent<Rigidbody2D>().mass = 70f;
        }

        if (attacker.tag == "Player" && currenthealth <= turnToEnemyThreshold){
            aiBehaviour.ChangeTarget(attacker);
        } else if (attacker.tag == "Enemy"){
            aiBehaviour.ChangeTarget(attacker);
        }
    }
}

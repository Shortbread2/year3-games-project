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
    private MeleeAttacks meleeAttack;
    private float turnToEnemyThreshold;
    private GameObject theAttacker;
    private bool selfDefense = false;
    // Start is called before the first frame update
    void Start()
    {
        aiBehaviour = this.GetComponent<AIBehaviour>();
        meleeAttack = this.GetComponent<MeleeAttacks>();
        //aiBehaviour.enabled = false;
        meleeAttack.enabled = false;
        currenthealth = health;
        displayhealth = health;
        turnToEnemyThreshold = health * 0.6f;
        healthBar.SetMaxHealth(health);
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selfDefense == false){
            aiBehaviour.WanderAround();
            aiBehaviour.checkIfStuck();
        }
        if(theAttacker != null){
            if (!theAttacker.activeSelf){
                selfDefense = false;
                meleeAttack.enabled = false;
                aiBehaviour.SeenTarget = false;

                // seems to be an bug - when npc finished attacking the condittions to move again and stop attacking doesnt happen
                animator.SetBool("isAttacking",false);
                aiPathfinder.canMove = true;
            }
        }
    }
    public void TakeDamage(float damage, GameObject attacker)
    {
        theAttacker = attacker;
        selfDefense = true;
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
            aiBehaviour.enabled = false;
            meleeAttack.enabled = false;
        }
        
        if (attacker.tag == "Player" && currenthealth <= turnToEnemyThreshold){
            aiBehaviour.ChangeTarget(attacker);
        } else if (attacker.tag == "Enemy"){
            aiBehaviour.ChangeTarget(attacker);
        }

        meleeAttack.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor; // cos handles gives erros since can only use handlies with this import
#endif

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

        //get waypoints
        if (waypointGroup != null){
            foreach (Transform waypoint in waypointGroup.transform){
                waypointsList.Add(waypoint.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selfDefense == false && aiBehaviour.isMobile){
            if(useWaypoints == true){
                aiBehaviour.WaypointMovement(waypointsList,doneWaypoints);
                if(repeatWaypoints == true && waypointsList.Count == doneWaypoints.Count){
                    doneWaypoints.Clear();
                } else if(waypointsList.Count == doneWaypoints.Count){
                    useWaypoints = false;
                    aiBehaviour.resetSearchBase();
                }
            }else{
                aiBehaviour.WanderAround();
            }
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

    private void OnDrawGizmos(){
        if (useWaypoints && waypointsList != null){
            for (int i = 0; i < waypointsList.Count - 1; i++){

                Handles.color = Color.blue;
                //Handles.DrawWireDisc(waypointsList[i].transform.position, new Vector3(0, 0, 1), 0.2f);

                Handles.color = Color.red;
                Handles.DrawLine(waypointsList[i].transform.position, waypointsList[i+1].transform.position);
            }
        }
    }
}

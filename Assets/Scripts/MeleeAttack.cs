using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class MeleeAttack : MeleeAttacks
{   private Animator animator;
    private Transform targetTransform;
    private AIBehaviour AIBehaviour;
    private float lastAction;
    // Start is called before the first frame update
    void Start()
    {
        setTarget(GameObject.FindGameObjectWithTag("Player"));
        targetTransform = getTarget().GetComponent<Transform>();
        animator = GetComponent<Animator>();
        AIBehaviour = GetComponent<AIBehaviour>();
    }

    // the debug circle to see the attack distance
    private void OnDrawGizmos(){
        Handles.color = Color.red;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), attackdistance);
    }

    // Updates the animator and checks if it should be able to move while attacking
    void Update()
    {
        if (AIBehaviour.enabled == true){
            setTarget(AIBehaviour.GetTarget());
            targetTransform = getTarget().transform;
        
            if (checkIfInAttackRange())
                {
                    if (AIBehaviour.lvl1Int == true && AIBehaviour.lvl2Int == false){
                        AIBehaviour.aiPathfinder.canMove = false;
                    }
                    if (Time.time - lastAction > 1 / attackSpeed && AIBehaviour.SeenTarget == true)
                    {
                        animator.SetBool("isAttacking",true);
                        animator.SetFloat("AttackSpeed",attackSpeed);
                        lastAction = Time.time;
                    }
                } else {
                    animator.SetBool("isAttacking",false);
                    AIBehaviour.aiPathfinder.canMove = true;
                }
        }
    }

    // accessed by script on attack animation in animator - a bit convoluted but easiest way to synch attack anim with nockback and health reduction
    public void attack(){
        if(checkIfInAttackRange()){
            if (getTarget().tag == "Player"){
                getTarget().GetComponent<PlayerHealth>().PlayerTakeDamage(Damage);
            } else if (getTarget().tag == "NPC"){
                getTarget().GetComponent<NPC>().TakeDamage(Damage, this.gameObject);
            } else if (getTarget().tag == "Enemy"){
                getTarget().GetComponent<enemy>().TakeDamage(Damage);
            }
       
            Rigidbody2D rb = getTarget().GetComponent<Rigidbody2D>();
            Vector2 knockback = (targetTransform.position - transform.position).normalized * knockbackForce;
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }
        
    }

    // this script is also used in a script on the animator to check if the attack animation is still on going and the player is still in range
    public bool checkIfInAttackRange(){
        if (Vector2.Distance(transform.position, targetTransform.position) <= attackdistance){
            return true;
        }
        else{
            return false;
        }
    }
}

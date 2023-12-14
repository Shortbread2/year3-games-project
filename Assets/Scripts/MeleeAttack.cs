using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class MeleeAttack : MeleeAttacks
{
    private GameObject target;
    private Animator animator;
    private Transform targetTransform;
    private AIBehaviour AIBehaviour;
    private float lastAction;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = target.GetComponent<Transform>();
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
            target = AIBehaviour.GetTarget();
            targetTransform = target.transform;
        
            if (Vector2.Distance(transform.position, targetTransform.position) <= attackdistance)
                {
                if (AIBehaviour.lvl1Int == true && AIBehaviour.lvl2Int == false){
                    AIBehaviour.aiPathfinder.canMove = false;
                }
                    if (Time.time - lastAction > 1 / attackSpeed)
                    {
                        //Debug.Log("attack!!");
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
        if (target.tag == "Player"){
            target.GetComponent<PlayerHealth>().PlayerTakeDamage(Damage);
        }
       
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        Vector2 knockback = (targetTransform.position - transform.position).normalized * knockbackForce;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}

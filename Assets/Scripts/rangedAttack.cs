using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class rangedAttack : RangedAttacks
{
    private Animator animator;
    private Transform targetTransform;
    private AIBehaviour AIBehaviour;
    private float lastAction;
    public GameObject projectileprefab;

    void Start()
    {
        setTarget(GameObject.FindGameObjectWithTag("Player"));
        targetTransform = getTarget().GetComponent<Transform>();
        animator = GetComponent<Animator>();
        AIBehaviour = GetComponent<AIBehaviour>();

        foreach (Transform child in transform)
        {
            if (child.name.ToLower().Contains("firepoint")){
                firepoints.Add(child);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), attackRadius);
        Handles.color = Color.white;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), attackOffsetRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (AIBehaviour.enabled == true){
            setTarget(AIBehaviour.GetTarget());
            targetTransform = getTarget().transform;
        
            if (checkIfInAttackRange())
                {
                    AIBehaviour.aiPathfinder.canMove = false;
                    animator.SetBool("isMovingOveride",false);
                    if (Time.time - lastAction > 1 / attackSpeed && AIBehaviour.SeenTarget == true)
                    {
                        animator.SetBool("isAttacking",true);
                        animator.SetFloat("AttackSpeed",attackSpeed);

                        lastAction = Time.time;
                    }
                } else if(Vector2.Distance(transform.position, targetTransform.position) <= attackOffsetRadius){
                    AIBehaviour.aiPathfinder.canMove = false;

                    animator.SetBool("isAttacking",false);
                    animator.SetBool("isMovingOveride",true);
                    Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;
                    Vector3 newPosition = transform.position - directionToTarget * (AIBehaviour.speed * 0.6f) * Time.deltaTime;

                    // Update the NPC's position
                    transform.position = newPosition;
                }else {
                    animator.SetBool("isAttacking",false);
                    animator.SetBool("isMovingOveride",false);
                    AIBehaviour.aiPathfinder.canMove = true;
                }
        }
    }
    public void attack(){
        if (checkIfInAttackRange()){
            foreach(Transform firepoint in firepoints){
                Vector3 newRotationOffset = new Vector3(0,0,Random.Range(-rotationOffset, rotationOffset));
                GameObject projectile = Instantiate(projectileprefab, firepoint.position, firepoint.rotation * Quaternion.Euler(newRotationOffset * 100));
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce((firepoint.up * BulletForce) + new Vector3(0,newRotationOffset.z,0), ForceMode2D.Impulse);

                projectile.GetComponent<homingMissile>().damage = Damage*2;
                projectile.GetComponent<homingMissile>().gameObjctToAvoid = gameObject;
                projectile.GetComponent<homingMissile>().target = getTarget().transform;
            }
        }
        
    }

    public bool checkIfInAttackRange(){
        float distance = Vector2.Distance(transform.position, targetTransform.position);
        if (distance <= attackRadius && distance > attackOffsetRadius){
            return true;
        }
        else{
            return false;
        }
    }
}

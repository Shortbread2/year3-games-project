using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class EnemyMelee : MeleeAttacks
{
    private GameObject player;
    private Animator animator;
    private Transform target;
    private enemyBehaviour enemyBehaviour;
    private enemy enemy;
    private float Damage = 5f;
    private float lastAction;
    private AnimatorClipInfo[] animatorinfo;
    private string current_animation = "";
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemyBehaviour = GetComponent<enemyBehaviour>();
        enemy = GetComponent<enemy>();
    }

    // the debug circle to see the attack distance
    private void OnDrawGizmos(){
        Handles.color = Color.red;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), attackdistance);
    }

    // Updates the animator and checks if it should be able to move while attacking
    void Update()
    {
        Damage = enemy.damage;
        if (Vector2.Distance(transform.position, target.position) <= attackdistance)
            {
            if (enemyBehaviour.lvl1Int == true && enemyBehaviour.lvl2Int == false){
                enemyBehaviour.aiPathfinder.canMove = false;
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
                enemyBehaviour.aiPathfinder.canMove = true;
            }
    }

    // accessed by script on attack animation in animator - a bit convoluted but easiest way to synch attack anim with nockback and health reduction
    public void attack(){
        player.GetComponent<PlayerHealth>().PlayerTakeDamage(Damage);
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        Vector2 knockback = (target.position - transform.position).normalized * knockbackForce;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}

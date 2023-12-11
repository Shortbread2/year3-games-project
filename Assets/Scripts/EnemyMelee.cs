using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class EnemyMelee : MonoBehaviour
{
    GameObject player;
    private Animator animator;
    private Transform target;
    private enemyBehaviour enemyBehaviour;
    private enemy enemy;
    public float attackdistance = 0.3f;
    private float Damage = 5f;
    public float attackSpeed = 10f;
    public float lastAction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemyBehaviour = GetComponent<enemyBehaviour>();
        enemy = GetComponent<enemy>();
    }

    private void OnDrawGizmos(){
        Handles.color = Color.red;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), attackdistance);
    }

    // Update is called once per frame
    void Update()
    {
        Damage = enemy.damage;
        //animator.GetComponent<DestroyOnExitAnim>();
        if (Vector2.Distance(transform.position, target.position) <= attackdistance)
            {
            if (enemyBehaviour.lvl1Int == true){
                enemyBehaviour.aiPathfinder.canMove = false;
            }
                if (Time.time - lastAction > 1 / attackSpeed)
                {
                    //Debug.Log("attack!!");
                    animator.SetBool("isAttacking",true);
                    player.GetComponent<PlayerHealth>().PlayerTakeDamage(Damage);
                    lastAction = Time.time;


                }
            } else {
                animator.SetBool("isAttacking",false);
                enemyBehaviour.aiPathfinder.canMove = true;
            }
    }
}

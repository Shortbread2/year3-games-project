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
    public float attackdistance = 0.3f;
    public int Damage = 1;
    public float attackSpeed = 10;
    public float lastAction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.GetComponent<Transform>();
        animator = this.GetComponent<Animator>();
    }

    private void OnDrawGizmos(){
        Handles.color = Color.red;
        //Handles.Disc(Quaternion.identity,transform.position, Vector3.zero, 0.3f, false, 0.3f);
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), attackdistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= attackdistance)
            {
                if (Time.time - lastAction > 1 / attackSpeed)
                {
                    //Debug.Log("attack!!");
                    animator.SetBool("isAttacking",true);
                    player.GetComponent<PlayerHealth>().PlayerTakeDamage(Damage);
                    lastAction = Time.time;


                }
            } else {animator.SetBool("isAttacking",false);}
    }
}

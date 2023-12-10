using System.Threading;
using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
    using UnityEditor; // cos handles gives erros since can only use handlies with this import
#endif

public class enemyBehaviour : MonoBehaviour
{
    private Vector3 playerlocation;
    private Vector3 playerLastLocation;
    private Vector3 direction;
    public AIBase aiPathfinder;
    public Transform lastSeenNode;
    private Animator animator;
    Collider2D col;
    int Distance = 100;
    GameObject player;
    public float speed = 0.2f;
    public float stopdistance = 3;
    private Transform target;
    public bool SeenPlayer = false;
    public bool moveTolastKnowPos = false;
    public bool IsNotMobile;
    public bool lvl1Int = false;
    public bool lvl2Int = false;
    public float wanderRange = 0.2f;
    public float rangeForRandPoint = 0.2f;
    private bool moving = false;
    private Vector3 randomPoint = new Vector3(0,0,0);
    public float time;
    public float lastAction;
    public float searchSpeed = 10;
    [SerializeField] private LayerMask EnemyLayer;
    void Start()
    {
        aiPathfinder.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        col = this.GetComponent<Collider2D>();
        animator = this.GetComponent<Animator>();
        target = player.GetComponent<Transform>();
        aiPathfinder.maxSpeed = speed;
    }
    void Update()
    {
        time = Time.time;
        animator.SetBool("isMoving", moving);
        animator.SetFloat("LookDir", transform.position.x - lastSeenNode.transform.position.x);
        playerlocation = new Vector2(player.transform.position.x, player.transform.position.y);
        direction = playerlocation - this.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, Vector3.Distance(this.gameObject.transform.position, player.transform.position), EnemyLayer);

        Debug.DrawLine(transform.position, player.transform.position, Color.red);
        //If something was hit.
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                SeenPlayer = true;
            }
            if (!hit.collider.CompareTag("Player") && SeenPlayer == true)
            {
                playerLastLocation = playerlocation;
                SeenPlayer = false;
                moveTolastKnowPos = true;
            }
        }
        if (moveTolastKnowPos == true){
            lastSeenNode.position = playerLastLocation;

            if (Vector2.Distance(transform.position, lastSeenNode.position) <= 0.3f){
                if (Vector2.Distance(transform.position, target.position) > 0.3f){
                    aiPathfinder.enabled = false;
                    moving = false;
                    WanderAround();
                }
            }
        }
        if (SeenPlayer == true)
        {
            lastSeenNode.position = target.position;
            moveTolastKnowPos = false;
            if (IsNotMobile == false)
            {
                MoveToPlayer();
            } else {
                aiPathfinder.enabled = false;
                moving = false;
            }
        }
    }
    void MoveToPlayer()
    {
        if (Vector2.Distance(transform.position, target.position) < Distance)
        {
            if (Vector2.Distance(transform.position, target.position) > stopdistance)
            {
                aiPathfinder.enabled = true;
                moving = true;
            }
            if (Vector2.Distance(transform.position, target.position) < stopdistance)
            {
                aiPathfinder.enabled = false;
                moving = true;
                transform.position = Vector2.MoveTowards(transform.position, -target.position, speed * Time.deltaTime);
            }
        }
    }
    public void SetAttackPause(bool value){
        if (!lvl2Int){
            aiPathfinder.canMove = !value;
        }
    }

    private void OnDrawGizmos(){
        Handles.color = Color.blue;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), wanderRange); // draw wanderRange
    }

    void WanderAround(){
        Debug.Log(1 / searchSpeed);
        if (Time.time - lastAction > 1 / searchSpeed)
        {
            randomPoint = this.gameObject.transform.position + new Vector3(Random.insideUnitCircle.x * wanderRange, Random.insideUnitCircle.x * wanderRange, 0); //random point in a circle 
            RaycastHit2D hit2 = Physics2D.Raycast(this.gameObject.transform.position, (randomPoint-this.transform.position), Vector3.Distance(this.gameObject.transform.position, randomPoint), EnemyLayer);
            while (transform.position != randomPoint){
                if (hit2.collider == null && (Time.time - lastAction > 1 / searchSpeed) == false){
                    Debug.Log("move!");
                    transform.position = Vector2.MoveTowards(transform.position, randomPoint, speed * Time.deltaTime);
                } else {
                    transform.position = Vector2.MoveTowards(transform.position, hit2.collider.transform.position, speed * Time.deltaTime);
                }
            }
            lastAction = Time.time;
        }
        Debug.DrawRay(this.gameObject.transform.position, (randomPoint-this.transform.position), Color.blue);
    }
}
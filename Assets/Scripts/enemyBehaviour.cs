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
    private Vector3 playerLastLocation = new Vector3(0,0,0);
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
    public bool searching = false;
    public float viewRange = 0.2f;
    private float OriginalViewRange = 0.2f;
    public float wanderRange = 0.2f;
    private bool moving = false;
    private Vector3 randomPoint = new Vector3(0,0,0);
    private Vector3 searchBase = new Vector3(0,0,0);
    public float time;
    private float lastAction = -999999f;
    private float lastStuckCheck;
    private float prevDistance = 0f;
    public bool enableRngSearchSpeed = false;
    public float searchSpeed = 10;
    [SerializeField] private LayerMask EnemyLayer;
    void Start()
    {
        //aiPathfinder.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        col = this.GetComponent<Collider2D>();
        animator = this.GetComponent<Animator>();
        target = player.GetComponent<Transform>();
        aiPathfinder.maxSpeed = speed;
        searchBase = transform.position;
        OriginalViewRange = viewRange;
    }
    void Update()
    {

        if (transform.position.x - lastSeenNode.transform.position.x > 0){
            GetComponent<SpriteRenderer>().flipX = true;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
        }

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
                if (Vector2.Distance(transform.position, target.position) <= viewRange){
                    SeenPlayer = true;
                    if (lvl2Int){
                        viewRange = OriginalViewRange + 0.6f;
                    }
                }
            }
            if (!hit.collider.CompareTag("Player") && SeenPlayer == true)
            {
                playerLastLocation = playerlocation;
                SeenPlayer = false;
                moveTolastKnowPos = true;
            } else if(!hit.collider.CompareTag("Player") && moveTolastKnowPos == false)
            {
                viewRange = OriginalViewRange;
                WanderAround();
            }
        } else {
            viewRange = OriginalViewRange;
            WanderAround();
        }
        if (SeenPlayer == true)
        {
            searching = false;
            lastSeenNode.position = target.position;
            if (IsNotMobile == false)
            {
                MoveToPlayer();
            } else {
                aiPathfinder.enabled = false;
                moving = false;
            }
        } else{
            if (searching == false){
                lastSeenNode.position = playerLastLocation;
                searchBase = lastSeenNode.position;
            }

            // if the enemy loses the player and reaches the last known position it will begin to search in that area
            if (Vector2.Distance(transform.position, lastSeenNode.position) <= 0.3f || searching == true){
                if (Vector2.Distance(transform.position, target.position) > 0.3f){
                    WanderAround();
                }
            }
        }
        if (aiPathfinder.canMove == false){
            moving = false;
        }
        checkIfStuck();
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
            // if entity is too close to target
            if (Vector2.Distance(transform.position, target.position) < stopdistance)
            {
                aiPathfinder.enabled = false;
                moving = true;
                transform.position = Vector2.MoveTowards(transform.position, -target.position, speed * Time.deltaTime);
            }
        }
    }

    // for anim to stop entities movement when attacking
    public void SetAttackPause(bool value){
        if (!lvl2Int){
            aiPathfinder.canMove = !value;
        }
    }

    private void OnDrawGizmos(){
        Handles.color = Color.blue;
        Handles.DrawWireDisc(searchBase, new Vector3(0, 0, 1), wanderRange); // draw wanderRange

        Handles.color = Color.black;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), viewRange); // draw viewRange
    }

    void WanderAround(){
        moving = true;
        searching = true;

        if (enableRngSearchSpeed == true){
            searchSpeed = Random.Range(0.03f, 0.3f);
        }

        if (Time.time - lastAction > 1 / searchSpeed)
        {
            randomPoint = searchBase + new Vector3(Random.insideUnitCircle.x * wanderRange, Random.insideUnitCircle.y * wanderRange, 0); //random point in a circle
            lastAction = Time.time;
        }
        lastSeenNode.position = randomPoint;
        if (Vector2.Distance(transform.position, lastSeenNode.position) <= 0.3f)
        {
            moving = false;
        } else{
            lastAction = Time.time;
        }
    }

    // not the best fix but eh
    void checkIfStuck(){
        float theDistance = Vector2.Distance(transform.position, lastSeenNode.position);
        if (Time.time - lastStuckCheck > 0.8f)
        {
            if (prevDistance == theDistance && moving == true){
                Debug.Log("STUCK!");
                randomPoint = transform.position;
                lastSeenNode.position = transform.position;
            }
            lastStuckCheck = Time.time;
            prevDistance = theDistance;
        }
        //lastSeenNode.position = randomPoint;
    }
}
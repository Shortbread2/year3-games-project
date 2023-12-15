using System.Threading;
using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
    using UnityEditor; // cos handles gives erros since can only use handlies with this import
#endif

public class AIBehaviour : MonoBehaviour
{
    private Vector3 targetlocation;
    private Vector3 targetLastLocation = new Vector3(0,0,0);
    private Vector3 direction;
    public AIBase aiPathfinder;
    public Transform lastSeenNode;
    private Animator animator;
    private Collider2D col;
    int Distance = 100;
    private GameObject target;
    [SerializeField]
    private string targetTag = "Player";
    public float speed = 0.2f;
    public float stopdistance = 3;
    private Transform targetTransform;
    public bool SeenTarget = false;
    private bool moveTolastKnowPos = false;
    public bool IsNotMobile;
    public bool lvl1Int = false;
    public bool lvl2Int = false;
    private bool searching = false;
    public float viewRange = 0.2f;
    private float OriginalViewRange = 0.2f;
    public float wanderRange = 0.2f;
    private bool moving = false;
    private Vector3 randomPoint = new Vector3(0,0,0);
    private Vector3 searchBase = new Vector3(0,0,0);
    private float lastAction = -999999f;
    private float lastStuckCheck;
    private float prevDistance = 0f;
    [SerializeField] private LayerMask EnemyLayer;
    void Start()
    {
        //aiPathfinder.enabled = false;
        target = GameObject.FindGameObjectWithTag(targetTag);
        col = this.GetComponent<Collider2D>();
        animator = this.GetComponent<Animator>();
        targetTransform = target.GetComponent<Transform>();
        aiPathfinder.maxSpeed = speed;
        searchBase = transform.position;
        OriginalViewRange = viewRange;
    }
    void Update()
    {
        // for the animation
        if (transform.position.x - lastSeenNode.transform.position.x > 0){
            GetComponent<SpriteRenderer>().flipX = true;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // talk to animator
        animator.SetBool("isMoving", moving);
        animator.SetFloat("LookDir", transform.position.x - lastSeenNode.transform.position.x);
        targetlocation = new Vector2(target.transform.position.x, target.transform.position.y);
        direction = targetlocation - this.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, Vector3.Distance(this.gameObject.transform.position, target.transform.position), EnemyLayer);

        Debug.DrawLine(transform.position, target.transform.position, Color.red);
        //If something was hit.
        if (hit.collider != null)
        {
            // checks if raycast hit target and is in view range
            if (hit.collider.CompareTag(targetTag))
            {
                if (Vector2.Distance(transform.position, targetTransform.position) <= viewRange){
                    SeenTarget = true;
                    if (lvl2Int){
                        viewRange = OriginalViewRange + 0.6f;
                    }
                }
            }
            // if the target was found at one point but is now lost
            if (!hit.collider.CompareTag(targetTag) && SeenTarget == true)
            {
                targetLastLocation = targetlocation;
                SeenTarget = false;
                moveTolastKnowPos = true;
            } else if(!hit.collider.CompareTag(targetTag) && moveTolastKnowPos == false)
            {
                viewRange = OriginalViewRange;
                WanderAround();
            }
        } else {
            viewRange = OriginalViewRange;
            WanderAround();
        }
        if (SeenTarget == true)
        {
            searching = false;
            lastSeenNode.position = targetTransform.position;
            if (IsNotMobile == false)
            {
                MoveToTarget();
            } else {
                aiPathfinder.enabled = false;
                moving = false;
            }
        } else{
            if (searching == false){
                lastSeenNode.position = targetLastLocation;
                searchBase = lastSeenNode.position;
            }

            // if the enemy loses the target and reaches the last known position it will begin to search in that area
            if (Vector2.Distance(transform.position, lastSeenNode.position) <= 0.3f || searching == true){
                if (Vector2.Distance(transform.position, targetTransform.position) > 0.3f){
                    WanderAround();
                }
            }
        }
        if (aiPathfinder.canMove == false){
            moving = false;
        }
        checkIfStuck();
    }
    void MoveToTarget()
    {
        if (Vector2.Distance(transform.position, targetTransform.position) < Distance)
        {
            if (Vector2.Distance(transform.position, targetTransform.position) > stopdistance)
            {
                aiPathfinder.enabled = true;
                moving = true;
            }
            // TODO - if entity is too close to targetTransform
            if (Vector2.Distance(transform.position, targetTransform.position) < stopdistance)
            {
                aiPathfinder.enabled = false;
                moving = true;
                //transform.position = Vector2.MoveTowards(transform.position, -targetTransform.position, speed * Time.deltaTime);
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

    public void WanderAround(){
        moving = true;
        searching = true;

            float searchSpeed = Random.Range(3f, 30f);

        if (Time.time - lastAction > searchSpeed)
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
    public void checkIfStuck(){
        float theDistance = Vector2.Distance(transform.position, lastSeenNode.position);
        if (Time.time - lastStuckCheck > 0.3f)
        {
            if (prevDistance == theDistance && moving == true){
                Debug.Log("STUCK!");
                randomPoint = transform.position;
                lastSeenNode.position = transform.position;
            }
            lastStuckCheck = Time.time;
            prevDistance = theDistance;
        }
    }
    public void ChangeTarget(GameObject newObj){
        target = newObj;
        targetTag = newObj.tag;
        targetTransform = target.GetComponent<Transform>();
    }
    public GameObject GetTarget(){
        return target;
    }
}
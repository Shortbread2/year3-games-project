using System.Threading;
using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
    using UnityEditor; // cos handles gives erros since can only use handlies with this import
#endif

// MUST READ: This script and all the ai rely on the A* Pathfinding Project for the pathing: https://arongranberg.com/astar/
public class AIBehaviour : MonoBehaviour
{
    // i defenetly need to clean this
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
    public bool isMobile = true;
    public bool SeenTarget = false;
    private bool moveTolastKnowPos = false;
    public bool lvl1Int = false;
    public bool lvl2Int = false;
    private bool searching = false;
    public float viewRange = 0.2f;
    private float OriginalViewRange = 0.2f;
    public float wanderRange = 0.2f;
    private bool moving = true;
    private Vector3 randomPoint = new Vector3(0,0,0);
    private Vector3 searchBase;
    private float lastAction = -999999f;
    private float lastStuckCheck;
    private float prevDistance = 0f;
    [SerializeField] private LayerMask EnemyLayer;

    // kinda a mess but it initialises everything, some of them is just in case to make sure no errors/bugs appear tbh
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
        targetLastLocation = transform.position;
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
            if (isMobile == true)
            {
                MoveToTarget();
            } else {
                aiPathfinder.enabled = false;
                moving = false;
            }
        } else{
            // 
            if (searching == false){
                lastSeenNode.position = targetLastLocation;
                searchBase = lastSeenNode.position;
            }

            // if the enemy loses the target and reaches the last known position it will begin to search in that area,
            // this is done by checking the distance between the entity and the player is in a certain distance and the same is done for the distance between the entity and the lastSeenNode
            if ((Vector2.Distance(transform.position, lastSeenNode.position) <= 0.3f || searching == true) && isMobile){
                if (Vector2.Distance(transform.position, targetTransform.position) > 0.3f){
                    WanderAround();
                }
            }
        }

        //canMove amd isMobile do nearly the exact same thing since they both are used to stop the entity from moving but canMove is used in attacking scripts like meleeAttack,
        // as i dont want the enemy to constantly move while attacking (well sometimes since some enemies are like berserkers) and since canMove gets re-enabled after each attack i needed a second boolean value to keep the entity from moving.
        if (aiPathfinder.canMove == false || isMobile == false){
            moving = false;
        }

        //the checking area
        checkIfStuck();
        CheckEntitiesInRange();
    }
    void MoveToTarget()
    {
        if (Vector2.Distance(transform.position, targetTransform.position) < Distance)
        {
            // main peice of code that enables the ai/entity to move
            if (Vector2.Distance(transform.position, targetTransform.position) > stopdistance)
            {
                aiPathfinder.enabled = true;
                moving = true;
            }
            if (Vector2.Distance(transform.position, targetTransform.position) < stopdistance)
            {
                aiPathfinder.enabled = false;
                moving = true;
                //transform.position = Vector3.MoveTowards(transform.position, -targetTransform.position, speed * Time.deltaTime); // need to be tested more
            }
        }
    }



    // makes it so u can see where what ranges are and what size and other stuff
    private void OnDrawGizmos(){
        Handles.color = Color.blue;
        Handles.DrawWireDisc(searchBase, new Vector3(0, 0, 1), wanderRange); // draw wanderRange

        Handles.color = Color.red;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), viewRange); // draw viewRange
    }

    // given the wanderRange find a random point in the wanderRange area and make that the location the entity should go to
    public void WanderAround(){
        moving = true;
        searching = true;

        // the searchSpeed basically tells them how long they have to wait until the next point is generated
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

    public void WaypointMovement(List<GameObject> waypointsList, List<GameObject> doneWaypoints){
        moving = true;
        searching = false;

        targetLastLocation = transform.position;

        // reversing the for loop makes it so the for loop always ends on the left most element
        for(int i = waypointsList.Count-1;i>=0;i--){
            if (waypointsList[i].activeSelf && !doneWaypoints.Contains(waypointsList[i])){
                //Debug.Log(waypointsList[i].name);
                lastSeenNode.position = waypointsList[i].transform.position;

                if (Vector2.Distance(transform.position, lastSeenNode.position) <= 0.3f)
                {
                    moving = false;
                    doneWaypoints.Add(waypointsList[i]);
                }
            }
        }
    }

    //searchBase needs to be reset after certain actions
    public void resetSearchBase(){
        Debug.Log("testing");
        targetLastLocation = transform.position;
        lastSeenNode.position = targetLastLocation;
        searchBase = lastSeenNode.position;
    }

    // not the best fix but eh, if the entitiy is moving on the spot or is absurdly slow then resets the point at which the entity was supposed to move to
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
        isMobile = true;
        target = newObj;
        targetTag = newObj.tag;
        targetTransform = target.GetComponent<Transform>();
    }
    public GameObject GetTarget(){
        return target;
    }

    public List<GameObject> entitiesInRange = new List<GameObject>();
    // on enter add entitiy from list
    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.tag != other.tag)
        {
            entitiesInRange.Add(other.gameObject);
        }
    }

// checks which entity that can be attacked has the smallest distance
// TODO - add bias towards low health and not just distance
// TODO - need to set a priority list (so the enemy instantly targets the player when in range) probs another bool variable again
private float minDistance=99999999;
    private void CheckEntitiesInRange(){
        float distance = 0;
        foreach(GameObject entity in entitiesInRange){
            distance = Vector2.Distance(targetTransform.position, entity.transform.position);
            if(distance < minDistance){
                if (entity.CompareTag("NPC") && this.tag != entity.tag)
                {
                    ChangeTarget(entity.gameObject);
                    minDistance = distance;
                }
                if (entity.CompareTag("Enemy") && this.tag != entity.tag)
                {
                    ChangeTarget(entity.gameObject);
                    minDistance = distance;
                }
                if (entity.CompareTag("Player") && this.tag == "Enemy")
                {
                    ChangeTarget(entity.gameObject);
                    minDistance = distance;
                }
            }
        }
        minDistance = 99999999;

        if(target != null){
            if (!target.activeSelf){
                // after target is dead, a new target is needed or there will be an error, so change to default target which is player then quickly put them into search mode/ wander mode
                ChangeTarget(GameObject.FindGameObjectWithTag("Player"));
                SeenTarget = false;
                searching = true;
            }
        }
    }
    // on exit remove entitiy from list
    void OnTriggerExit2D(Collider2D other)
    {
        if (entitiesInRange.Contains(other.gameObject)){
            entitiesInRange.Remove(other.gameObject);
        }
    }
}
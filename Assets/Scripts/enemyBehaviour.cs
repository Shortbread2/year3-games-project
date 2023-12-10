using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public Vector3 playerlocation;
    public Vector3 playerLastLocation;
    public Vector3 direction;
    public Behaviour aiPathfinder;
    public Transform lastSeenNode;
    private Animator animator;
    Collider2D col;
    int Distance = 100;
    GameObject player;
    public float speed = 5;
    public float stopdistance = 3;
    private Transform target;
    public bool SeenPlayer = false;
    public bool moveTolastKnowPos = false;
    public bool IsNotMobile = false;
    private bool moving = false;
    [SerializeField] private LayerMask EnemyLayer;
    void Start()
    {
        aiPathfinder.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        col = this.GetComponent<Collider2D>();
        animator = this.GetComponent<Animator>();
        target = player.GetComponent<Transform>();
    }
    void Update()
    {
        animator.SetBool("isMoving", moving);
        //Debug.Log(transform.position.x - lastSeenNode.transform.position.x);
        animator.SetFloat("LookDir", transform.position.x - lastSeenNode.transform.position.x);
        playerlocation = new Vector2(player.transform.position.x, player.transform.position.y);
        direction = playerlocation - this.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, Vector3.Distance(this.gameObject.transform.position, player.transform.position), EnemyLayer);

        Debug.DrawLine(transform.position, player.transform.position, Color.red);
        //If something was hit.
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider);
            if (hit.collider.CompareTag("Player"))
            {
                SeenPlayer = true;
            }
            if (!hit.collider.CompareTag("Player") && SeenPlayer == true)
            {
                //Debug.Log(transform.position);
                Debug.Log(playerlocation);
                playerLastLocation = playerlocation;
                SeenPlayer = false;
                moveTolastKnowPos = true;
            }
        }
        if (moveTolastKnowPos == true){
            lastSeenNode.position = playerLastLocation;

            if (Vector2.Distance(transform.position, lastSeenNode.position) <= 0.3f){
                if (Vector2.Distance(transform.position, target.position) > 0.3f){
                    // TODO - initiate search or go back to idle
                    aiPathfinder.enabled = false;
                    moving = false;
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
}
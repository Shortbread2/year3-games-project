using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public Vector3 playerlocation;
    public Vector3 playerLastLocation;
    public Vector3 direction;
    public Behaviour aiPathfinder;
    public Transform lastSeenNode;
    Collider2D col;
    int Distance = 100;
    GameObject player;
    public float speed = 5;
    public float stopdistance = 3;
    private Transform target;
    public bool SeenPlayer = false;
    public bool moveTolastKnowPos = false;
    public bool IsNotMobile = true;
    [SerializeField] private LayerMask EnemyLayer;
    void Start()
    {
        aiPathfinder.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        col = this.GetComponent<Collider2D>();
        target = player.GetComponent<Transform>();
    }
    void Update()
    {
        playerlocation = new Vector2(player.transform.position.x, player.transform.position.y);
        direction = playerlocation - this.transform.position;

        //Cast a ray in the direction specified in the inspector.
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, Vector3.Distance(this.gameObject.transform.position, player.transform.position), EnemyLayer);

        Debug.DrawRay(this.gameObject.transform.position, direction, Color.green);
        Debug.DrawLine(this.transform.position, player.transform.position, Color.red);
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
                    // initiate search or go back to idle
                    aiPathfinder.enabled = false;
                }
            }
        }
        if (SeenPlayer == true)
        {
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
                lastSeenNode.position = target.position;
                aiPathfinder.enabled = true;
                //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, target.position) < stopdistance)
            {
                aiPathfinder.enabled = false;
                transform.position = Vector2.MoveTowards(transform.position, -target.position, speed * Time.deltaTime);
            }
        }
    }
}
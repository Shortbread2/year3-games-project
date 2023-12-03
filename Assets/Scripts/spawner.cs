using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float spawndelay;
    public float stopdistance = 6;
    private Transform target;
    private int rand;
    private Vector2 spawnPos;
    public Vector3 playerlocation;
    public Vector3 direction;
    Collider2D col;
    GameObject player;
    [SerializeField] private LayerMask EnemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        col = this.GetComponent<Collider2D>();
        target = player.GetComponent<Transform>();
           
    }
    void Update()
    {
        playerlocation = new Vector2(player.transform.position.x, player.transform.position.y + 1);
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
                if (Time.time - spawndelay > 2)
                {
                    SpawnObject();
                    spawndelay = Time.time;
                }
            }
        }
    }

    public void SpawnObject()
    {
        if (Vector2.Distance(transform.position, target.position) < stopdistance)
        {
            rand = Random.Range (0, 5);
            switch (rand)
            {
                case 0:
                    spawnPos.y -= 6;
                    Instantiate(ObjectToSpawn, spawnPos, transform.rotation);
                    break;
                case 1:
                    spawnPos.x -= 6;
                    spawnPos.y -= 3f;
                    Instantiate(ObjectToSpawn, spawnPos, transform.rotation);
                    break;
                case 2:
                    spawnPos.x -= 6;
                    spawnPos.y += 3f;
                    Instantiate(ObjectToSpawn, spawnPos, transform.rotation);
                    break;
                case 3:
                    spawnPos.y += 6;
                    Instantiate(ObjectToSpawn, spawnPos, transform.rotation);
                    break;
                case 4:
                    spawnPos.x += 6;
                    spawnPos.y += 3f;
                    Instantiate(ObjectToSpawn, spawnPos, transform.rotation);
                    break;
                default:
                    spawnPos.x += 6;
                    spawnPos.y -= 3f;
                    Instantiate(ObjectToSpawn, spawnPos, transform.rotation);
                    break;
            }
            spawnPos = transform.position;
        }
    }
}

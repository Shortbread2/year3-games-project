using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MUST READ - this script was influenced by Pix ans Dev for the rocket movement on youtube at https://www.youtube.com/watch?v=Qqf3yRj0VNA
public class homingMissile : MonoBehaviour
{
    [SerializeField]
    private float speed=20f;
    [SerializeField]
    private float steer=30f;
    public Transform target;
    private Rigidbody2D rb;
    [SerializeField] 
    private LayerMask EnemyLayer;
    public GameObject gameObjctToAvoid;
    // not gunna set it private might be useful in the future for other scripts
    public string tagToAvoid = "";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameObjctToAvoid != null){
            tagToAvoid = gameObjctToAvoid.tag;
        }
    }

    private void FixedUpdate(){
        if (target != null && target.gameObject.activeSelf && target.tag != tagToAvoid){
            rb.velocity = transform.up * speed * Time.fixedDeltaTime * 10f;
            Vector2 direction = (target.position - transform.position).normalized;
            float rotationSeer = Vector3.Cross(transform.up,direction).z;
            rb.angularVelocity = rotationSeer * steer * 10f;
        }
        
    }
    

    [SerializeField]
    private Animator animator;
    public float damage = 20f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        // to make sure the animation playing is still
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = new Vector3(0, 0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        if (collision.gameObject.tag == this.gameObject.tag){
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (animator != null){
            // there is a script in the animator to destroy objects on a certain animation ending, setting this trigger will play that animation
            animator.SetTrigger("hitCollider");
        } else {
            Destroy(gameObject);
        }

        // missile can hit any and hurt regardless of who fired the missile
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemy>().TakeDamage(damage);
        }
        if (collision.gameObject.tag == "NPC")
        {
            collision.gameObject.GetComponent<NPC>().TakeDamage(damage, gameObjctToAvoid);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(damage);
        }

    }

    // will pick a target based on the first entity in range and that it isnt told to avoid, the missile will find a target and not change since i dont want it to be smart
    void OnTriggerEnter2D(Collider2D other)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, other.transform.position - this.transform.position, Vector3.Distance(this.gameObject.transform.position, other.transform.position), EnemyLayer);
        
        if (this.tag != other.tag && hit.collider != null && hit.collider.CompareTag(other.tag) && other.tag != tagToAvoid && target == null)
        {
            target = other.gameObject.transform;
        }
    }
}

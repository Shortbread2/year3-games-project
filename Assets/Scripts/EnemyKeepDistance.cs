using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeepDistance : MonoBehaviour
{
    private enemyBehaviour enemyBehaviour;

    void Start(){
        enemyBehaviour = GetComponent<enemyBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"){
            Debug.Log("tooClose!");
            //transform.position = Vector2.MoveTowards(transform.position, -other.gameObject.transform.position, enemyBehaviour.speed * Time.deltaTime);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}

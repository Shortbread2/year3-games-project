using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infinite : MonoBehaviour
{
    public float offset2;
    public float offset1;
    public GameObject landPrefab; // Reference to the land prefab

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject, 1);
            Vector2 newPos = new Vector2(transform.position.x + offset1, transform.position.y + offset2);
            Instantiate(landPrefab, newPos, Quaternion.identity);
        }
    }
}

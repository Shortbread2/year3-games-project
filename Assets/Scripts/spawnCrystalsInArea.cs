using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif



public class spawnCrystalsInArea : MonoBehaviour
{
    public float radius = 0.3f;

    public GameObject PickupCollectible;
    public GameObject entityToSpawn;
    public GameObject player;
    private bool isSpawned = false;
    private bool playerInArea = false;


    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= radius && isSpawned == false)
            {
                PickupCollectible.SetActive(true);
                playerInArea = true;
                if (Input.GetKeyDown("f"))
                {
                    isSpawned = true;
                    Instantiate(entityToSpawn, transform.position, Quaternion.identity);
                    PickupCollectible.SetActive(false);
                }
            }
            if (Vector2.Distance(transform.position, player.transform.position) > radius && playerInArea == true)
            {
                Debug.Log("player left");
                playerInArea = false;
                PickupCollectible.SetActive(false);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif



public class spawnCrystalsInArea : MonoBehaviour
{
    public float radius = 0.3f;
    public GameObject entityToSpawn;
    public GameObject player;
    private bool isSpawned = false;
    // Start is called before the first frame update

    private void OnDrawGizmos(){
        Handles.color = Color.red;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= radius && isSpawned == false && Input.GetKeyDown("f")){
            isSpawned = true;
            Instantiate(entityToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}

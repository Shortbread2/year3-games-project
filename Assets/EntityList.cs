using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityList : MonoBehaviour
{
    public GameObject[] entities;
    public int numOfEntities = 0;
    void Update()
    {
        numOfEntities = this.transform.childCount;
        entities = new GameObject[numOfEntities];
        for (int i = 0; i < numOfEntities; i++){
            entities[i] = this.transform.GetChild(i).gameObject;
        }
    }
}

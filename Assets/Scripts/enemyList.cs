using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyList : MonoBehaviour
{
    public GameObject[] enemies;
    public int numOfEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        numOfEnemies = this.transform.childCount;
        enemies = new GameObject[numOfEnemies];
        for (int i = 0; i < numOfEnemies; i++){
            enemies[i] = this.transform.GetChild(i).gameObject;
        }
    }
}

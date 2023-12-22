using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWomanInteraction : MonoBehaviour
{
    public void SpawnEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length < 0 )
        {
            Debug.Log("No enemy tags found");
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }
}

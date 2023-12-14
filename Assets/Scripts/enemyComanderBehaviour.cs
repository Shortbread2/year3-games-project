using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyComanderBehaviour : MonoBehaviour
{
    private enemyList enemyList;
    private int prevNumOfEnemies = 0;
    public List<GameObject> enemies = new List<GameObject>();
    private float lastAction = -999999f;
    public bool comanderLvl1 = false;
    public bool comanderLvl2 = false;
    public bool comanderLvl3 = false;
    public float buffDuration = 10f;
    public float buffCooldown = 30f;
    public bool buffDurationOver = false;
    public bool speedIncreaseBuff = false;
    public float speedIncreaseValue = 1.2f;
    public bool attackBuff = false;
    public float damageIncreaseValue = 1.2f;
    // TODO - alert nearby enemies , provide buff

    private AIBehaviour AIBehaviour;
    void Start()
    {
        enemyList = transform.parent.gameObject.GetComponent<enemyList>();
        AIBehaviour = GetComponent<AIBehaviour>();
    }


    void Update()
    {
            for (int i = 0; i < enemyList.numOfEnemies; i++){
                if (Vector2.Distance(transform.position, enemyList.enemies[i].transform.position) < AIBehaviour.viewRange && !enemies.Contains(enemyList.enemies[i]) && enemyList.enemies[i] != this.gameObject){
                    enemies.Add(enemyList.enemies[i]);
                }
                if (Vector2.Distance(transform.position, enemyList.enemies[i].transform.position) > AIBehaviour.viewRange && enemies.Contains(enemyList.enemies[i])){
                    enemies.Remove(enemyList.enemies[i]);
                }
        }

        if (AIBehaviour.SeenTarget == true && (comanderLvl1 || comanderLvl2 || comanderLvl3)){
            // special ability/buffs
            Ability();
        }

        if (comanderLvl2 || comanderLvl3){
            foreach(GameObject enemy in enemies){
                if (enemy.GetComponent<AIBehaviour>().SeenTarget == true){
                    // special ability/buffs
                    Ability();
                }
            }
        }
    }

    private void Ability(){
        AIBehaviour.SeenTarget = true;
        foreach(GameObject enemy in enemies){
            enemy.GetComponent<AIBehaviour>().SeenTarget = true;
        }

        if (speedIncreaseBuff){
            if (Time.time - lastAction > buffCooldown){
                foreach(GameObject enemy in enemies){
                    enemy.GetComponent<AIBehaviour>().speed *= speedIncreaseValue;
                }
                lastAction = Time.time;
                buffDurationOver = true;
            }
            if (Time.time - lastAction > buffDuration && buffDurationOver){
                foreach(GameObject enemy in enemies){
                    enemy.GetComponent<AIBehaviour>().speed /= speedIncreaseValue;
                }
                buffDurationOver = false;
            }
        }
        if (attackBuff){
            if (Time.time - lastAction > buffCooldown){
                foreach(GameObject enemy in enemies){
                    enemy.GetComponent<enemy>().damage *= damageIncreaseValue;
                }
                lastAction = Time.time;
                buffDurationOver = true;
            }
            if (Time.time - lastAction > buffDuration && buffDurationOver){
                foreach(GameObject enemy in enemies){
                    enemy.GetComponent<enemy>().damage /= damageIncreaseValue;
                }
                buffDurationOver = false;
            }
        }
    }
}

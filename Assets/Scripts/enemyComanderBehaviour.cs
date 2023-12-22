using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyComanderBehaviour : MonoBehaviour
{
    private EntityList entityList;
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

    private AIBehaviour AIBehaviour;
    void Start()
    {
        entityList = transform.parent.gameObject.GetComponent<EntityList>();
        AIBehaviour = GetComponent<AIBehaviour>();
    }

    // checks what nearby enemies are in range and adds them to an array
    void Update()
    {
            for (int i = 0; i < entityList.numOfEntities; i++){
                if (entityList.entities[i].tag == "Enemy"){
                    if (Vector2.Distance(transform.position, entityList.entities[i].transform.position) < AIBehaviour.viewRange && !enemies.Contains(entityList.entities[i]) && entityList.entities[i] != this.gameObject){
                        enemies.Add(entityList.entities[i]);
                    }
                    if (Vector2.Distance(transform.position, entityList.entities[i].transform.position) > AIBehaviour.viewRange && enemies.Contains(entityList.entities[i])){
                        enemies.Remove(entityList.entities[i]);
                    }
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

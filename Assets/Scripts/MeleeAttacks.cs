using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeAttacks : MonoBehaviour
{
    public float attackdistance = 0.3f;
    public float attackSpeed = 10f;
    public float attackDelay = 0.1f;
    public float knockbackForce = 3f; 
    public float Damage = 5f;
    private GameObject target;

    public void setTarget(GameObject gameObject){
        target = gameObject;
    }
    public GameObject getTarget(){
        return target;
    }
}

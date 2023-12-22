using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedAttacks : MonoBehaviour
{
    public float attackSpeed = 10f;
    public float knockbackForce = 3f; 
    public float Damage = 5f;
    public float BulletForce = 0.5f;
    private GameObject target;
    public float rotationOffset = 0.3f;
    // the radius is the radius at which the entity will attack
    public float attackRadius = 0.3f;
    // the offsetRadius is the radius at which the entity will not attack (kinda like a mortar cant target anything too close to itself)
    public float attackOffsetRadius = 0f;
    public List<Transform> firepoints  = new List<Transform>();

    public void setTarget(GameObject gameObject){
        target = gameObject;
    }
    public GameObject getTarget(){
        return target;
    }
}

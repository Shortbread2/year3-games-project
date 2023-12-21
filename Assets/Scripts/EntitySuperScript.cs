using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySuperScript : MonoBehaviour
{
    public float health = 100;
    public float displayhealth;
    [HideInInspector]
    public float speed;
    public bool useWaypoints = false;
    public bool repeatWaypoints = false;
    public GameObject waypointGroup;
    public List<GameObject> waypointsList = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> doneWaypoints = new List<GameObject>();
}
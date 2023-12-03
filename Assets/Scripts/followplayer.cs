﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    public float speed = 5; 
    public float stopdistance = 3;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stopdistance) 
        { 
  transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
      
    }
}

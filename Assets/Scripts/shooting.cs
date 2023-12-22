﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{

    public Transform firepoint;
    public List<GameObject> projectileprefabs  = new List<GameObject>();
    public float damage = 5;
    public float FireRate = 10;
    public float BulletForce = 0.5f;
    private float lastfired;
    [SerializeField]
    private float rotationOffset = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                Vector3 newRotationOffset = new Vector3(0,0,Random.Range(-rotationOffset, rotationOffset));
                GameObject projectile = Instantiate(projectileprefabs[Random.Range(0,projectileprefabs.Count)], firepoint.position, firepoint.rotation * Quaternion.Euler(newRotationOffset * 100));
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                Debug.Log((firepoint.up+newRotationOffset));
                rb.AddForce((firepoint.up * BulletForce) + new Vector3(0,newRotationOffset.z,0), ForceMode2D.Impulse);
                projectile.GetComponent<PlayerProjectile>().damage = damage;
                lastfired = Time.time;


            }

        }
    }

}

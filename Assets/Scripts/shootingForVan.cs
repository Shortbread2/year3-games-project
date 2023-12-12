using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingVan : MonoBehaviour
{

    public Transform firepoint;
    public GameObject projectile1prefab;
    public float FireRate = 10;
    public float BulletForce = 0.5f;
    private float lastfired;

    // Update is called once per frame
    void Update()
    {


        if (Time.time - lastfired > 1 / FireRate)
        {
            GameObject projectile1 = Instantiate(projectile1prefab, firepoint.position, firepoint.rotation);
            Rigidbody2D rb = projectile1.GetComponent<Rigidbody2D>();
            rb.AddForce(firepoint.up * BulletForce, ForceMode2D.Impulse);
            lastfired = Time.time;

        }


    }

}

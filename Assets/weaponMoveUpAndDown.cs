using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponMoveUpAndDown : MonoBehaviour
{
    [SerializeField]
    private float Speed = 0.1f;
    private Vector3 Direction = new Vector3(0.0f, -1.0f, 0.0f);
    private float targetTime = 1.0f;

    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            Direction = -Direction;
            targetTime += 1.0f;
        }
        transform.Translate(Speed * Direction * Time.deltaTime);
    }
}

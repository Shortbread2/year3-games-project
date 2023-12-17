using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject ppc;

    // Start is called before the first frame update
    void Update()
    {
        if (ppc != null)
        {
            // Set the initial position
            transform.position = new Vector3(ppc.transform.position.x, transform.position.y, transform.position.z);

        }


        if (ppc == null)
        {
            Destroy(gameObject);
        }
    }
}

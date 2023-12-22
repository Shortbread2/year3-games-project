using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openinglevel4Door : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy1;
    public GameObject Enemy2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Animator doorAnimator = gameObject.GetComponent<Animator>();
            doorAnimator.SetBool("DoorOpen", true);

        }
    }
}

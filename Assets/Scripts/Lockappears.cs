using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAppears : MonoBehaviour
{
    public GameObject twistingHand;
    public GameObject unlockedCuffshand;
    private Vector3 lastHandPos;
    private Quaternion lastHandRotation;
    private Vector3 pickingHandPos;
    private Quaternion pickingHandRotation;
    public GameObject lockSolved;
    public GameObject uncuffed;

    public GameObject noPicking;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bent paperclip 1"))
        {
            lastHandPos = collision.gameObject.transform.position;
            lastHandRotation = collision.gameObject.transform.rotation;

            Destroy(collision.gameObject);
            twistingHand.SetActive(true);
            twistingHand.transform.position = lastHandPos;
            twistingHand.transform.rotation = lastHandRotation;
        }
    }

    void Update()
    {
        if (lockSolved.activeSelf == true)
        {
            Destroy(gameObject);
            pickingHandPos = gameObject.transform.position;
            pickingHandRotation = gameObject.transform.rotation;
            unlockedCuffshand.transform.position = pickingHandPos;
            unlockedCuffshand.transform.rotation = pickingHandRotation;
            Destroy(noPicking);
            unlockedCuffshand.SetActive(true);
            uncuffed.SetActive(true);
        }
    }
}

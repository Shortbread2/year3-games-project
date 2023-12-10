using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unhide : MonoBehaviour
{

    public GameObject SolvedLock;
    public GameObject FinalPaperclip;
    public GameObject PickingHand;
    public GameObject VanDoors;

    public GameObject Exit;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            SolvedLock.SetActive(false);
            FinalPaperclip.SetActive(false);
            PickingHand.SetActive(false);
            VanDoors.SetActive(true);
            Exit.SetActive(true);
        }
    }
}

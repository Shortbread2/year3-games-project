using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickManager : MonoBehaviour
{
    public GameObject VanDoors;

    public GameObject Exit;

    public void showStuff()

    {

        VanDoors.SetActive(true);
        Exit.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showButton : MonoBehaviour
{
    public GameObject Button;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            Button.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

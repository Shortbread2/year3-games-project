using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line for the Image component

public class EnableCanvasComponent : MonoBehaviour
{
    public GameObject intro;

    void Start()
    {

        if (!intro.activeSelf)
        {
            GetComponent<Image>().enabled = true;

        }
    }
}

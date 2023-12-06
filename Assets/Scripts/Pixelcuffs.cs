using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixelcuffs : MonoBehaviour
{
    public GameObject PixelCuffs;
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            PixelCuffs.SetActive(true);

        }
    }


}

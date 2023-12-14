using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automaticAStarScan : MonoBehaviour
{
    public AstarPath astarPath;
    public float lastAction;
    public float timeInterval = 10f;

    // to update the a* grid automatically every n seconds. However causes some lag
    void Update()
    {
        if (Time.time - lastAction > timeInterval)
        {
            //astarPath.ScanAsync();
            astarPath.Scan();
            lastAction = Time.time;
        }
    }
}

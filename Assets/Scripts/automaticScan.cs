using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automaticAStarScan : MonoBehaviour
{
    public AstarPath astarPath;
    public float lastAction;
    public float timeInterval = 10f; // 1f is every second etc
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAction > timeInterval)
        {
            astarPath.ScanAsync();
            //astarPath.Scan();
            lastAction = Time.time;
        }
    }
}

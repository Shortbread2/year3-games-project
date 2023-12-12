using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public GameObject followHoops;
    public float minX = 74.74f;
    public float maxX = 75.8f;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        // Set initial values
        startTime = Time.time;
        SetRandomJourneyLength();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, followHoops.transform.position.y, -2);

        if (Time.time - startTime > journeyLength)
        {
            transform.position = new Vector3(Random.Range(minX, maxX), followHoops.transform.position.y, -2);
            startTime = Time.time;
            SetRandomJourneyLength();
        }
    }

    void SetRandomJourneyLength()
    {
        // Set journeyLength to a random value between 0.5 and 1 second
        journeyLength = Random.Range(0.5f, 1f);
    }
}

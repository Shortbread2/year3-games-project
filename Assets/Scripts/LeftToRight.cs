using System.Collections;
using UnityEngine;

public class LeftToRight : MonoBehaviour
{
    public GameObject followHoops;
    public float distance = 2f;
    public float speed = 4f;

    private float minX;
    private float maxX;
    private float startTime;
    private float nextPauseTime;
    private float nextSetMinMaxTime;


    void Start()
    {
        startTime = Time.time;
        SetNextPauseTime();
        SetNextSetMinMaxTime();
        SetMinMaxPositions(); // Initial call to set initial positions
    }

    void Update()
    {

        if (Time.time >= nextSetMinMaxTime)
        {
            SetMinMaxPositions();
            SetNextSetMinMaxTime();
        }

        if (followHoops != null)
        {
            float targetX = followHoops.transform.position.x;

            if (Time.time >= nextPauseTime)
            {
                StartCoroutine(Pause(Random.Range(0.5f, 1f)));
                SetNextPauseTime();
            }

            float oscillation = Mathf.Sin((Time.time - startTime) * speed) * distance;
            float xPosition = Mathf.Clamp(targetX + oscillation, minX, maxX);

            transform.position = new Vector3(xPosition, followHoops.transform.position.y, -2);
        }
    }

    IEnumerator Pause(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    void SetNextPauseTime()
    {
        nextPauseTime = Time.time + Random.Range(2f, 5f);
    }

    void SetMinMaxPositions()
    {
        // Randomly set minX and maxX, ensuring minX is less than or equal to maxX
        minX = Random.Range(74.74f, 75.75f);
        maxX = Random.Range(minX, 75.75f);

        Debug.Log("minX: " + minX + ", maxX: " + maxX);
    }

    void SetNextSetMinMaxTime()
    {
        nextSetMinMaxTime = Time.time + Random.Range(1f, 2f);
    }
}

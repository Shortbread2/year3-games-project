using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float minX = 74.8f;
    public float maxX = 75.8f;
    public float moveDistance = 10f;
    public float minDelay = 2f;
    public float maxDelay = 5f;
    public float smoothSpeed = 2f;

    void Start()
    {
        StartCoroutine(RandomMove());
    }

    IEnumerator RandomMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            float targetX = Random.Range(minX, maxX);
            float direction = (targetX - transform.position.x) / Mathf.Abs(targetX - transform.position.x);
            float distance = Random.Range(0f, moveDistance);

            Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            Vector3 newPosition = transform.position + Vector3.right * direction * distance;

            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
                yield return null;
            }
        }
    }
}

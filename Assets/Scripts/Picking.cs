using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 7f;
    public GameObject MiniMiniPuzzle;
    public GameObject MiniMiniPuzzlePaperclip;

    void Start()
    {
        StartCoroutine(RotateObjectCoroutine());
        MiniMiniPuzzle.SetActive(true);
        MiniMiniPuzzlePaperclip.SetActive(true);
    }

    IEnumerator RotateObjectCoroutine()
    {
        while (true)
        {
            // Rotate from left to right
            for (int i = 0; i < 7; i++)
            {
                transform.Rotate(Vector3.up * rotationSpeed);
                yield return null;
            }

            float randPause = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(randPause); // Pause for a random duration between 0.1 and 0.4 seconds

            // Rotate from right to left
            for (int i = 0; i < 7; i++)
            {
                transform.Rotate(Vector3.up * -rotationSpeed);
                yield return null;
            }

            randPause = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(randPause); // Pause for a random duration between 0.1 and 0.4 seconds
        }
    }
}

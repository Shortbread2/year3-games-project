using System.Collections;
using UnityEngine;

public class ShipInteract : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject cutscene;
    public GameObject arrowtoNextScene;

    public Timer timerScript; 

    private bool cutsceneActivated = false;
    private float delayBeforeArrow = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision with ship");
            if (timerScript != null)
            {
                if (timerScript.isEnginePuzzleComplete)
                {
                    Time.timeScale = 0f;
                    if (cutscene != null)
                    {
                        cutscene.SetActive(true);
                        cutsceneActivated = true;

                        StartCoroutine(ActivateArrowAfterDelay());
                    }
                }
                else
                {
                    if (dialogue != null)
                        dialogue.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogue != null)
                dialogue.SetActive(false);
        }
    }

    private IEnumerator ActivateArrowAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeArrow);

        if (arrowtoNextScene != null && cutsceneActivated)
        {
            arrowtoNextScene.SetActive(true);
        }
    }
}

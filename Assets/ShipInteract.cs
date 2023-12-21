using UnityEngine;

public class ShipInteract : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject cutscene;
    public GameObject arrowtoNextScene;

    public Timer timerScript; // Assign in the Inspector

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
                        Invoke("ActivateArrowToNextScene", delayBeforeArrow);
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

    private void ActivateArrowToNextScene()
    {
        if (arrowtoNextScene != null && cutsceneActivated)
        {
            arrowtoNextScene.SetActive(true);
        }
    }
}

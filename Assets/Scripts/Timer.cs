using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 60f;
    public GameObject GAMEOVER;

    void Update()
    {
        GameObject taggedObject = GameObject.FindGameObjectWithTag("Engine Pieces");

        if (taggedObject != null)
        {
            if (timer >= 1f)
            {
                timer -= Time.deltaTime;
                UpdateTimerText();

                // Decrease transparency every second
                if (Mathf.FloorToInt(timer) != Mathf.FloorToInt(timer + Time.deltaTime))
                {
                    DecreaseTransparency();
                }
            }
            else
            {
                GAMEOVER.SetActive(true);
                string timerString = string.Format("{0:00}", "00");
                timerText.text = timerString;
                // Stop counting and decrease the transparency of GAMEOVER to 0


            }
        }
        else
        {
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            // Decrease alpha by 3
            color.a = 255f;
            renderer.color = color;
        }
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(timer);
        string timerString = string.Format("{0:00}", seconds);
        timerText.text = timerString;
    }

    void DecreaseTransparency()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            Color color = renderer.color;
            // Decrease alpha by 3
            color.a = Mathf.Max(0, color.a - 3f / 255f);
            renderer.color = color;
        }
    }
}

using UnityEngine;
using TMPro;

public class MiningTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float Timed = 90f;

    public PlayerCollection PlayerCollection;
    public GameObject TimerParent;
    public GameObject GameOverPanel;

    private bool isTimerRunning = true;

    void Update()
    {
        if (GameOverPanel.activeSelf == true)
        {
            // Don't update the timer if the game is over
            return;
        }
        if (isTimerRunning)
        {
            if (Timed >= 1f)
            {
                Timed -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                // Timer reached zero or was stopped
                string timerString = string.Format("{0:00}", "00");
                timerText.text = timerString;
                Destroy(TimerParent);

                if (isTimerRunning)
                {
                    int gemCount = 0;
                    if (PlayerCollection.collectiblesDictionary.TryGetValue("Gem", out gemCount))
                    {
                        if (gemCount < 5)
                        {
                            GameOverPanel.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(Timed);
        string timerString = string.Format("{0:00}", seconds);
        timerText.text = timerString;
    }

    public bool getTimeIsZero()
    {
        return Timed == 0;
    }

    // Method to stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }
}

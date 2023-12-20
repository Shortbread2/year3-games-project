using UnityEngine;
using TMPro;
using UnityEngine.UI; // Add this for Image

public class MiningTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float Timed = 10f;

    public PlayerCollection PlayerCollection;
    public GameObject TimerParent;

    void Update()
    {
        if (Timed >= 1f)
        {
            Timed -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            string timerString = string.Format("{0:00}", "00");
            timerText.text = timerString;
            Destroy(TimerParent);


            int gemCount = 0;
            if (PlayerCollection.collectiblesDictionary.TryGetValue("Gem", out gemCount))
            {
                if (gemCount < 5)
                {
                    //GameOverPanel.SetActive(true);
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
        if (Timed == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

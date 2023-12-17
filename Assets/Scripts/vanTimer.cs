using UnityEngine;
using TMPro;
using UnityEngine.UI; // Add this for Image

public class vanTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 85f;

    public GameObject panel;

    void Update()
    {
        if (timer >= 1f)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
            // Adjust the alpha value of the panel's image
            Color panelColor = panel.GetComponent<Image>().color;
            panelColor.a += 0.00017f; // Increase alpha by 0.05 each frame
            panel.GetComponent<Image>().color = panelColor;
        }
        else
        {
            string timerString = string.Format("{0:00}", "00");
            timerText.text = timerString;


        }
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(timer);
        string timerString = string.Format("{0:00}", seconds);
        timerText.text = timerString;
    }
}

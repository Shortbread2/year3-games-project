using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class vanTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timer = 60f;
    public GameObject panel;

    public GameObject removeInstructions;


    void Update()
    {
        if (timer >= 1f)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            // Adjust the alpha value of the panel's image at specific times
            if (timer <= 16f && timer > 15f)
            {
                SetPanelAlpha(230f);
            }
            else if (timer <= 13f && timer > 11f)
            {
                SetPanelAlpha(0f);
            }
            else if (timer <= 11f && timer > 9f)
            {
                SetPanelAlpha(240f);
            }
            else if (timer <= 9f && timer > 7f)
            {
                SetPanelAlpha(0f);
            }
            else if (timer <= 7f && timer > 6f)
            {
                SetPanelAlpha(245f);
            }
            else if (timer <= 6f && timer > 3f)
            {
                SetPanelAlpha(0f);
            }
            else if (timer <= 3f && timer > 2f)
            {
                SetPanelAlpha(250f);
            }
            else if (timer <= 2f && timer > 1f)
            {
                SetPanelAlpha(0f);
            }
            else if (timer <= 1f)
            {
                SetPanelAlpha(255f);
            }
        }
        else
        {
            string timerString = string.Format("{0:00}", "00");
            timerText.text = timerString;
            Destroy(removeInstructions);
            Destroy(gameObject);
        }
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.FloorToInt(timer);
        string timerString = string.Format("{0:00}", seconds);
        timerText.text = timerString;
    }

    void SetPanelAlpha(float alpha)
    {
        Color panelColor = panel.GetComponent<Image>().color;
        panelColor.a = alpha / 255f;
        panel.GetComponent<Image>().color = panelColor;
    }
}

using UnityEngine;
using TMPro;

public class PasswordGame : MonoBehaviour
{
    public TextMeshProUGUI outputDisplay;
    private string enteredDigits = "";
    public string correctPassword = "1234";
    private bool displayTryAgain = false;

    public void AddDigit(string digit)
    {
        if (enteredDigits.Length < 4)
        {
            enteredDigits += digit;
            UpdateDisplay();
        }
    }

    public void Undo()
    {
        if (enteredDigits.Length > 0)
        {
            enteredDigits = enteredDigits.Substring(0, enteredDigits.Length - 1);
            UpdateDisplay();
        }
    }

    public void CheckPassword()
    {
        if (enteredDigits == correctPassword)
        {
            outputDisplay.text = "CORRECT";
        }
        else
        {
            displayTryAgain = true;
            enteredDigits = "";
        }
    }

    void UpdateDisplay()
    {
        outputDisplay.text = enteredDigits;
    }

    private void Update()
    {
        if (displayTryAgain)
        {
            StartCoroutine(DisplayTryAgain());
            displayTryAgain = false;
        }
    }

    System.Collections.IEnumerator DisplayTryAgain()
    {
        outputDisplay.text = "TRY AGAIN";
        yield return new WaitForSeconds(3f); 
        outputDisplay.text = enteredDigits; 
    }
}

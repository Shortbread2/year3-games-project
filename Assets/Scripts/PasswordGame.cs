using UnityEngine;
using TMPro;

public class PasswordGame : MonoBehaviour
{
    public TextMeshProUGUI outputDisplay;
    private string enteredDigits = "";
    public string correctPassword = "5321";
    private bool displayTryAgain = false;

    public GameObject secondPartEnv;
    public GameObject passwordGate;
    public GameObject mainCanvas;
    public InteractableObject interactableObjectActive;

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
            Time.timeScale = 1;
            secondPartEnv.SetActive(true);
            OpenDoor();
            mainCanvas.SetActive(false);
            passwordGate.GetComponent<BoxCollider2D>().enabled = false;

            if (interactableObjectActive != null)
            {
                Debug.Log("Interactive object set to false");
                interactableObjectActive.isScriptActive = false;
            }
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

    void OpenDoor()
    {
        {
            if (passwordGate != null)
            {
                Animator doorAnimator = passwordGate.GetComponent<Animator>();

                if (doorAnimator != null)
                {
                    doorAnimator.SetBool("DoorOpen", true);
                }
                else
                {
                    Debug.LogError("Animator component not found on PasswordGate GameObject!");
                }
            }
            else
            {
                Debug.LogError("PasswordGate GameObject reference not set!");
            }
        }
    }
}

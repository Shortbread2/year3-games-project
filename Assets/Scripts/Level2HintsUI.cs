using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2HintsUI : MonoBehaviour
{
    public GameObject Hint1Button;
    public GameObject Hint1Interface;

    public GameObject Hint2Button;
    public GameObject Hint2Interface;

    public GameObject Hint3Button;
    public GameObject Hint3Interface;

    public playerMovement PlayerMove;

    public void displayHint1()
    {
        Hint1Button.SetActive(false);
        Hint1Interface.SetActive(true);
        PlayerMove.isScriptActive = false;
    }

    public void closeHint1()
    {
        Hint1Button.SetActive(true);
        Hint1Interface.SetActive(false);
        PlayerMove.isScriptActive = true;
    }

    public void displayHint2()
    {
        Hint2Button.SetActive(false);
        Hint2Interface.SetActive(true);
        PlayerMove.isScriptActive = false;
    }

    public void closeHint2()
    {
        Hint2Button.SetActive(true);
        Hint2Interface.SetActive(false);
        PlayerMove.isScriptActive= true;
    }

    public void displayHint3()
    {
        Hint3Button.SetActive(false);
        Hint3Interface.SetActive(true);
        PlayerMove.isScriptActive = false;
    }

    public void closeHint3()
    {
        Hint3Button.SetActive(true);
        Hint3Interface.SetActive(false);
        PlayerMove.isScriptActive = true;
    }
}

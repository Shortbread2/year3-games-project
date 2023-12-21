using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceDisplayManager : MonoBehaviour
{
    [SerializeField] GameObject MainInventory;
    [SerializeField] GameObject PasswordPuzzle;
    [SerializeField] GameObject TetrisEnginePuzzle;
    [SerializeField] GameObject LockpickingPuzzle;
    [SerializeField] playerMovement PlayerMove;

    public GameObject parentOfEverything;

    public void openInventory()
    {
        MainInventory.SetActive(true);
        Time.timeScale = 0;
    }

    public void closeInventory()
    {
        MainInventory.SetActive(false);
        Time.timeScale = 1;
    }

    public void openPasswordPuzzle()
    {
        PasswordPuzzle.SetActive(true);
        Time.timeScale = 0;
    }

    public void closePasswordPuzzle()
    {
        PasswordPuzzle.SetActive(false);
        Time.timeScale = 1;
    }

    public void openLockpickingPuzzle()
    {
        LockpickingPuzzle.SetActive(true);
        Time.timeScale = 1;
        PlayerMove.enabled = false;
    }

    public void closeLockpickingPuzzle()
    {
        LockpickingPuzzle.SetActive(false);
        Time.timeScale = 1;
    }

    public void openTetrisEnginePuzzle()
    {
        TetrisEnginePuzzle.SetActive(true);
        PlayerMove.enabled = false;
        parentOfEverything.SetActive(false);
    }

    public void closeTetrisEnginePuzzle()
    {
        TetrisEnginePuzzle.SetActive(false);
        PlayerMove.enabled = true;
        parentOfEverything.SetActive(true);
    }

}

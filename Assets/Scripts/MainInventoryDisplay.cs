using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject MainInventory;

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
}

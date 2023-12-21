using System.Collections;
using UnityEngine;

public class CutScenes : MonoBehaviour
{

    [SerializeField] GameObject Inventory;
    void Start()
    {
        // Freeze time
        Time.timeScale = 0;
        Inventory.SetActive(true);

    }

    public void unfreezeTime()
    {
        Time.timeScale = 1;
        Inventory.GetComponent<Canvas>().sortingOrder = 2;
        Inventory.SetActive(false);
        
    }
}

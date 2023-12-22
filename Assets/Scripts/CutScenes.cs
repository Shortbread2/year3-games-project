using System.Collections;
using UnityEngine;
using static InventoryController;

public class CutScenes : MonoBehaviour
{
    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject cutScenePanel;
    [SerializeField] GameObject arrowToNextScene;
    [SerializeField] InventoryController inventoryController;

    void Start()
    {
        Time.timeScale = 0;
        Inventory.SetActive(true);
    }

    public void unfreezeTime()
    {
        Time.timeScale = 1;
        if (Inventory != null)
        {
            Inventory.GetComponent<Canvas>().sortingOrder = 2;
            Inventory.SetActive(false);
        }
        else
        {
            Inventory = null;
        }

        cutScenePanel.SetActive(false);
        arrowToNextScene.SetActive(false);

        if (inventoryController != null)
        {
            inventoryController.InsertItem(InventoryController.ItemType.Paperclip);
            inventoryController.InsertItem(InventoryController.ItemType.SonPhoto);
            inventoryController.InsertItem(InventoryController.ItemType.IDCard);
        }
        else
        {
            inventoryController = null;
        }
    }
}

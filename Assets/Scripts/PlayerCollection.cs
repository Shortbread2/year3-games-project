using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    public Dictionary<string, int> collectiblesDictionary = new Dictionary<string, int>();

    private InventoryController inventoryController;

    void Start()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        collectiblesDictionary.Add("Gem", 0);
        collectiblesDictionary.Add("Paperclip", 0);
        collectiblesDictionary.Add("IDCard", 0);
    }

    public void CollectItem(string itemType)
    {
        if (collectiblesDictionary.ContainsKey(itemType))
        {
            collectiblesDictionary[itemType]++;

            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCamera != null)
            {
                InventoryController inventoryController = mainCamera.GetComponent<InventoryController>();
                if (inventoryController != null)
                {
                    InsertItemIntoInventory(itemType, inventoryController);
                }
                else
                {
                    Debug.LogWarning("InventoryController script not found on the mainCamera object.");
                }
            }
            else
            {
                Debug.LogWarning("MainCamera object not found.");
            }
        }
        else
        {
            collectiblesDictionary.Add(itemType, 1);
        }
    }

    private void InsertItemIntoInventory(string itemType, InventoryController controller)
    {
        switch (itemType)
        {
            case "Gem":
                controller.InsertGemItem();
                break;
            case "Paperclip":
                controller.InsertPaperclipItem();
                break;
            case "IDCard":
                controller.InsertIDCardItem();
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        SaveCollectiblesData();
    }

    private void SaveCollectiblesData()
    {
        foreach (var pair in collectiblesDictionary)
        {
            PlayerPrefs.SetInt(pair.Key, pair.Value);
        }
    }
}

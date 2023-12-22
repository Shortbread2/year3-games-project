using System.Collections.Generic;
using UnityEngine;
using static InventoryController;

public class PlayerCollection : MonoBehaviour
{
    public Dictionary<string, int> collectiblesDictionary = new Dictionary<string, int>();

    void Start()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        // Initialize collectiblesDictionary with default values
        collectiblesDictionary.Add("Gem", 0);
        collectiblesDictionary.Add("Paperclip", 1);
        collectiblesDictionary.Add("IDCard", 1);
        collectiblesDictionary.Add("SonPhoto", 1);
    }

    public void CollectItem(ItemType itemType)
    {
        string itemName = itemType.ToString();

        if (collectiblesDictionary.ContainsKey(itemName))
        {
            collectiblesDictionary[itemName]++;

            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCamera != null)
            {
                InventoryController inventoryController = mainCamera.GetComponent<InventoryController>();
                if (inventoryController != null)
                {
                    inventoryController.InsertItem(itemType);
                }
                else
                {
                    Debug.LogWarning("InventoryController script not found on the MainCamera object.");
                }
            }
            else
            {
                Debug.LogWarning("MainCamera object not found.");
            }
        }
        else
        {
            collectiblesDictionary.Add(itemName, 1);
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

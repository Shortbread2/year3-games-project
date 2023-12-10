using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    public Dictionary<string, int> collectiblesDictionary = new();

    public int gemCount = 0;
    public int paperclipCount = 0;

    public void CollectItem(string itemType)
    {
        if (collectiblesDictionary.ContainsKey(itemType))
        {
            collectiblesDictionary[itemType]++;

            if (itemType == "Gem")
            {
                gemCount++;
            }
            else if (itemType == "Paperclip")
            {
                paperclipCount++;
            }
        }
        else
        {
            collectiblesDictionary.Add(itemType, 1);
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
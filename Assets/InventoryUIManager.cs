using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<Image> inventoryItems; // List to store inventory items

    private void Start()
    {
        // Find all objects with the 'slots' tag and fetch their Image components
        GameObject[] slotObjects = GameObject.FindGameObjectsWithTag("slots");
        inventoryItems = new List<Image>();

        // Collect Image components from objects with the 'slots' tag
        foreach (GameObject slotObject in slotObjects)
        {
            Image image = slotObject.GetComponent<Image>();
            if (image != null)
            {
                inventoryItems.Add(image);
            }
        }

        // Set the initial opacity to 0.5 for all inventory items
        SetOpacityForInventoryItems(0.8f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set opacity to 100% for all inventory items when hovered
        SetOpacityForInventoryItems(1.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Set opacity to 50% for all inventory items when not hovered
        SetOpacityForInventoryItems(0.8f);
    }

    private void SetOpacityForInventoryItems(float opacity)
    {
        foreach (Image item in inventoryItems)
        {
            Color itemColor = item.color;
            itemColor.a = opacity; // Set alpha value
            item.color = itemColor; // Apply the new color
        }
    }
}

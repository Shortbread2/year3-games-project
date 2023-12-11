using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<Image> inventoryItems;

    private void Start()
    {
        GameObject[] slotObjects = GameObject.FindGameObjectsWithTag("slots");
        inventoryItems = new List<Image>();

        foreach (GameObject slotObject in slotObjects)
        {
            Image image = slotObject.GetComponent<Image>();
            if (image != null)
            {
                inventoryItems.Add(image);
            }
        }

        SetOpacityForInventoryItems(0.8f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetOpacityForInventoryItems(1.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetOpacityForInventoryItems(0.8f);
    }

    private void SetOpacityForInventoryItems(float opacity)
    {
        foreach (Image item in inventoryItems)
        {
            Color itemColor = item.color;
            itemColor.a = opacity; 
            item.color = itemColor; 
        }
    }
}

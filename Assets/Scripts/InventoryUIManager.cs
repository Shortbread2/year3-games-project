using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<Image> inventoryItems;
    private Transform player;
    // where the item needs to be spawned
    private Transform playerItemLoc;

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

        // for shooting
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < player.childCount; i++)
        {
            GameObject child = player.GetChild(i).gameObject;
            //Do something with child
            if (child.name == "item location"){
                playerItemLoc = child.transform;
            }
        }

        SetOpacityForInventoryItems(0.8f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetOpacityForInventoryItems(1.0f);
        if (player != null && playerItemLoc.childCount == 1){
            playerItemLoc.GetChild(0).gameObject.GetComponent<shooting>().enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetOpacityForInventoryItems(0.8f);
        if (player != null && playerItemLoc.childCount == 1){
            playerItemLoc.GetChild(0).gameObject.GetComponent<shooting>().enabled = true;
        }
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

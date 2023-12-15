using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler
{
    //This script enables interactivity with the inventory grids

    InventoryController inventoryController; //ref to inventory controller
    ItemGrid itemGrid; // ref to item grid 

    private void Awake()
    {
        inventoryController = FindAnyObjectByType(typeof(InventoryController)) as InventoryController; 
        itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryController != null)
        {
            inventoryController.SelectedItemGrid = itemGrid; //set grid as selected grid
        }
    }
}

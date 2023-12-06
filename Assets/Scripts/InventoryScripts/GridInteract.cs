using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]
public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    ItemGrid itemGrid;

    private void Awake()
    {
        inventoryController = FindAnyObjectByType(typeof(InventoryController)) as InventoryController;
        itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryController != null)
        {
            inventoryController.SelectedItemGrid = itemGrid;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventoryController != null)
        {
            inventoryController.SelectedItemGrid = null;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public ItemGrid selectedItemGrid;
    
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlight.SetHighlighterParent(value);
        }
    }

    private InventoryItem selectedItem, overlapItem;
    private RectTransform rectTransform;
    private InventoryHighlight inventoryHighlight;
    private InventoryItem itemToHighlight;

    [SerializeField] private List<ItemData> items;
    [SerializeField] private ItemData gemItem, paperclipItem, IDCardItem, SonPhotoItem;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform canvasTransform;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        ItemIconDrag();
        if (Input.GetMouseButtonDown(1))
        {
            RotateItem();
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.ToggleHighlight(false);
            return;
        }

        try
        {
            HandleHighlight();
        }
        catch (IndexOutOfRangeException)
        {
            Debug.LogError("Index out of range exception occurred in HandleHighlight().");
        }

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    public enum ItemType
    {
        Gem,
        Paperclip,
        IDCard,
        SonPhoto
    }

    public void InsertItem(ItemType itemType)
    {
        if (selectedItemGrid == null) { return; }

        InventoryItem createdItem = CreateItemForInventory(itemType);
        InsertItemIntoInventory(createdItem);
    }

    private InventoryItem CreateItemForInventory(ItemType itemType)
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        switch (itemType)
        {
            case ItemType.Gem:
                inventoryItem.Set(gemItem);
                break;
            case ItemType.Paperclip:
                inventoryItem.Set(paperclipItem);
                break;
            case ItemType.IDCard:
                inventoryItem.Set(IDCardItem);
                break;
            case ItemType.SonPhoto:
                inventoryItem.Set(SonPhotoItem);
                break;
            default:
                break;
        }

        return inventoryItem;
    }

    private void InsertItemIntoInventory(InventoryItem itemToInsert)
    {
        selectedItem = null;

        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid != null)
        {
            selectedItemGrid.PlaceItemOnGrid(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
        }
        else
        {
            Debug.Log("No space available on the grid");
            Destroy(itemToInsert.gameObject);
        }
    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();

        try
        {
            if (selectedItem == null)
            {
                HandleItemHighlight(positionOnGrid);
            }
            else
            {
                HandleSelectedItemHighlight(positionOnGrid);
            }
        }
        catch (IndexOutOfRangeException)
        {
            
        }
    }

    private void HandleItemHighlight(Vector2Int positionOnGrid)
    {
        itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

        if (itemToHighlight != null)
        {
            inventoryHighlight.ToggleHighlight(true);
            inventoryHighlight.ResizeHighlighter(itemToHighlight);
            inventoryHighlight.SetHighlighterPosition(selectedItemGrid, itemToHighlight);
        }
        else
        {
            inventoryHighlight.ToggleHighlight(false);
        }
    }

    private void HandleSelectedItemHighlight(Vector2Int positionOnGrid)
    {
        inventoryHighlight.ToggleHighlight(selectedItemGrid.BoundaryCheck(
            positionOnGrid.x,
            positionOnGrid.y,
            selectedItem.Width,
            selectedItem.Height)
        );

        inventoryHighlight.ResizeHighlighter(selectedItem);
        inventoryHighlight.SetHighlighterPositionFromGridAndItem(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            try
            {
                PickUpItem(tileGridPosition);
            }
            catch (IndexOutOfRangeException)
            {
                
            }
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.Width - 1) * ItemGrid.TileSizeWidth / 2;
            position.y += (selectedItem.Height - 1) * ItemGrid.TileSizeHeight / 2;

        }
        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);

        if (complete)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);

        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
            rectTransform.SetAsLastSibling();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

    private void RotateItem()
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
    }
}
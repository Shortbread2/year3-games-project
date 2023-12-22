using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //This script handes all inventory interactions - Main script

    public ItemGrid selectedItemGrid; //currently selected inventory grid

    public ItemGrid SelectedItemGrid //Manage selected item grid and highlighter
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlight.SetHighlighterParent(value); //Set highlighter for selected grid
        }
    }

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;
    [SerializeField] List<ItemData> items;

    [SerializeField] ItemData gemItem;
    [SerializeField] ItemData paperclipItem;
    [SerializeField] ItemData IDCardItem;
    [SerializeField] ItemData SonPhotoItem;

    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;
    InventoryHighlight inventoryHighlight;

    InventoryItem itemToHighlight;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>(); //Access InventoryHighlight 
    }

    private void Update()
    {
        ItemIconDrag(); //Handle item dragging

        if (Input.GetMouseButtonDown(1))
        {
            RotateItem(); //Rotate on right-click
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.ToggleHighlight(false); //disable highlight if no grid selected
            return;
        }

        try
        {
            HandleHighlight();
        }
        catch (IndexOutOfRangeException)
        {
            //Debug.LogError("Index out of range exception occurred in HandleHighlight().");
        }

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress(); //Left click for item actions
        }
    }

    //Define Item types 
    public enum ItemType
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
    }

//Insert item of specific type into inventory
public void InsertItem(ItemType itemType)
{
    if (selectedItemGrid == null) { return; }

    InventoryItem createdItem = CreateItemForInventory(itemType); //Create item based on type
    InsertItemIntoInventory(createdItem); //Insert created item
}

//Create item for inventory based on item type
private InventoryItem CreateItemForInventory(ItemType itemType)
{
    InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>(); //Create item from prefab
    selectedItem = inventoryItem;

    rectTransform = inventoryItem.GetComponent<RectTransform>();
    rectTransform.SetParent(canvasTransform);
    rectTransform.SetAsLastSibling();

    // Set item data based on the provided type
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

// Insert item into the inventory grid
private void InsertItemIntoInventory(InventoryItem itemToInsert)
{
    if (selectedItemGrid == null) { return; }

    CreateIDCardItem();
    InsertItemIntoInventory();
}

public void InsertSonPhotoItem()
{
    if (selectedItemGrid == null) { return; }

    CreateSonPhotoItem();
    InsertItemIntoInventory();
}

private void InsertItemIntoInventory()
{
    InventoryItem itemToInsert = selectedItem;
    selectedItem = null;

    Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert); // Find available space in grid

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

    if (selectedItem == null)
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
    else
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
}


private void CreateGemItem()
{
    InventoryItem inventoryItem = CreateItemForInventory();

    inventoryItem.Set(gemItem);
}

private void CreatePaperclipItem()
{
    InventoryItem inventoryItem = CreateItemForInventory();

    inventoryItem.Set(paperclipItem);
}

private void CreateIDCardItem()
{
    InventoryItem inventoryItem = CreateItemForInventory();

    inventoryItem.Set(IDCardItem);
}

private void CreateSonPhotoItem()
{
    InventoryItem inventoryItem = CreateItemForInventory();
    inventoryItem.Set(SonPhotoItem);
}

private InventoryItem CreateItemForInventory()
{
    InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
    selectedItem = inventoryItem;

    rectTransform = inventoryItem.GetComponent<RectTransform>();
    rectTransform.SetParent(canvasTransform);
    rectTransform.SetAsLastSibling();

    return inventoryItem;
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
}
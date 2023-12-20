using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector] private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            inventoryHighlight.SetHighlighterParent(value);
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
        }

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    private void RotateItem()
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
    }

    public void InsertGemItem()
    {
        if (selectedItemGrid == null) { return; }

        CreateGemItem();
        InsertItemIntoInventory();
    }

    public void InsertPaperclipItem()
    {
        if (selectedItemGrid == null) { return; }

        CreatePaperclipItem();
        InsertItemIntoInventory();
    }

    public void InsertIDCardItem()
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
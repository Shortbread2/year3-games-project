using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float TileSizeWidth = 200;
    public const float TileSizeHeight = 200;

    [SerializeField] int gridSizeWidth = 5;
    [SerializeField] int gridSizeHeight = 5;

    InventoryItem[,] inventoryItemSlot;
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * TileSizeWidth, height * TileSizeHeight);
        rectTransform.sizeDelta = size;

        Debug.Log("Grid Size: " + size);
    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; }

        CleanGridReference(toReturn);

        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        int startGridX = item.onGridPositionX;
        int startGridY = item.onGridPositionY;

        for (int xx = 0; xx < item.Width; xx++)
        {
            for (int yy = 0; yy < item.Height; yy++)
            {
                int slotX = startGridX + xx;
                int slotY = startGridY + yy;

                if (IsWithinGrid(slotX, slotY))
                {
                    inventoryItemSlot[slotX, slotY] = null;
                }
            }
        }
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (!BoundaryCheck(posX, posY, inventoryItem.Width, inventoryItem.Height) ||
            !OverlapCheck(posX, posY, inventoryItem.Width, inventoryItem.Height, ref overlapItem))
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        PlaceItemOnGrid(inventoryItem, posX, posY);
        return true;
    }

    public void PlaceItemOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform itemRectTransform = inventoryItem.GetComponent<RectTransform>();
        itemRectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.Width; x++)
        {
            for (int y = 0; y < inventoryItem.Height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);
        itemRectTransform.anchoredPosition = position;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * TileSizeWidth + TileSizeWidth * inventoryItem.Width / 2;
        position.y = -posY * TileSizeHeight - TileSizeHeight * inventoryItem.Height / 2;

        return position;
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        Vector2 positionOnTheGrid = mousePosition - (Vector2)rectTransform.position;
        Vector2Int tileGridPosition = new Vector2Int(
            Mathf.FloorToInt(positionOnTheGrid.x / TileSizeWidth),
            Mathf.FloorToInt(-positionOnTheGrid.y / TileSizeHeight)
        );

        return tileGridPosition;
    }


    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    }
                    else if (overlapItem != inventoryItemSlot[posX + x, posY + y])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private bool IsWithinGrid(int slotX, int slotY)
    {
        return slotX >= 0 && slotX < gridSizeWidth && slotY >= 0 && slotY < gridSizeHeight;
    }

    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        return IsWithinGrid(posX, posY) && IsWithinGrid(posX + width - 1, posY + height - 1);
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridSizeHeight - itemToInsert.Height + 1;
        int width = gridSizeWidth - itemToInsert.Width + 1;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (CheckAvailableSpace(x, y, itemToInsert.Width, itemToInsert.Height))
                {
                    if (BoundaryCheck(x, y, itemToInsert.Width, itemToInsert.Height))
                    {
                        return new Vector2Int(x, y);
                    }
                    else
                    {
                        Debug.Log("Item exceeds grid boundaries");
                        return null;
                    }
                }
            }
        }
        Debug.Log("No space available in the grid");
        return null;
    }

    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
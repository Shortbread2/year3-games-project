using UnityEngine;

public class ItemGrid : MonoBehaviour
{

    //THIS SCRIPT IS RESPONSIBLE FOR GRID RELATED DATA

    public const float TileSizeWidth = 200; //set tile width
    public const float TileSizeHeight = 200; //set tile height

    [SerializeField] int gridSizeWidth = 5; //grid width (in tiles)
    [SerializeField] int gridSizeHeight = 5; //grid  height (in tiles)

    InventoryItem[,] inventoryItemSlot; 
    RectTransform rectTransform; 

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight); 
    }

    //METHOD: Initializes grid and sets its size
    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height]; // Create a new 2D array for the grid items
        Vector2 size = new Vector2(width * TileSizeWidth, height * TileSizeHeight); // Calculate the total size of the grid
        rectTransform.sizeDelta = size; Debug.Log("Grid Size: " + size); // Set the size of the RectTransform component

        Debug.Log("Grid Size: " + size); // Log the grid size for debugging
    }

    //METHOD: Pick up an item from a specific grid position
    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y]; //get item from specified grid position

        if (toReturn == null) { return null; } // If no item is found, return null


        CleanGridReference(toReturn); // Clean up the grid reference of the picked up item

        return toReturn; // Return the picked up item
    }

    //METHOD: Cleap up grid reference of an item being removed
    private void CleanGridReference(InventoryItem item)
    {
        int startGridX = item.onGridPositionX; // Starting X position of the item on the grid
        int startGridY = item.onGridPositionY; // Starting Y position of the item on the grid

        for (int xx = 0; xx < item.Width; xx++) // Calculate X position in the grid
        {
            for (int yy = 0; yy < item.Height; yy++) // Calculate Y position in the grid
            {
                int slotX = startGridX + xx;
                int slotY = startGridY + yy;

                if (IsWithinGrid(slotX, slotY))
                {
                    inventoryItemSlot[slotX, slotY] = null; // Clear the grid reference of the item
                }
            }
        }
    }

    //METHOD: Boolean of Place item on the grid
    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        // Check if the item can be placed without going out of grid boundaries or overlapping
        if (!BoundaryCheck(posX, posY, inventoryItem.Width, inventoryItem.Height) ||
            !OverlapCheck(posX, posY, inventoryItem.Width, inventoryItem.Height, ref overlapItem))
        {
            overlapItem = null; // Reset overlapItem reference
            return false; // Reset overlapItem reference
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem); //Clean up grid reference of overlapping item  
        }

        PlaceItemOnGrid(inventoryItem, posX, posY); // Place the item on the grid
        return true; // Item placed successfully
    }

    //METHOD: Actually placing an item on the grid
    public void PlaceItemOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform itemRectTransform = inventoryItem.GetComponent<RectTransform>(); // Get RectTransform component of the item
        itemRectTransform.SetParent(this.rectTransform);// Set the grid as the parent of the item

        // Loop through item dimensions and assign references to the grid slots
        for (int x = 0; x < inventoryItem.Width; x++)
        {
            for (int y = 0; y < inventoryItem.Height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem; // Set the grid reference to the item
            }
        }

        inventoryItem.onGridPositionX = posX; // Update the item's X position on the grid
        inventoryItem.onGridPositionY = posY; // Update the item's Y position on the grid

        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY); // Calculate item's position on the grid
        itemRectTransform.anchoredPosition = position; // Set the anchored position of the item
    }

    //METHOD: Calculate the position of an item on the grid
    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2(); // Initialize position vector
        position.x = posX * TileSizeWidth + TileSizeWidth * inventoryItem.Width / 2; // Calculate X position
        position.y = -posY * TileSizeHeight - TileSizeHeight * inventoryItem.Height / 2; // Calculate Y position

        return position;
    }

    //METHOD: Get the tile grid position based on mouse position
    public Vector2Int GetTileGridPosition(Vector2 mousePosition) 
    {
        Vector2 positionOnTheGrid = mousePosition - (Vector2)rectTransform.position; // Calculate X grid position
        Vector2Int tileGridPosition = new Vector2Int(
            Mathf.FloorToInt(positionOnTheGrid.x / TileSizeWidth), // Calculate X grid position
            Mathf.FloorToInt(-positionOnTheGrid.y / TileSizeHeight) // Calculate Y grid position
        );

        return tileGridPosition; // Return the grid position
    }

    //METHOD: check for overlap of items at the specified position
    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        // Loop through the area of the item and check for overlaps
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y]; // Set the reference to the overlapping item
                    }
                    else if (overlapItem != inventoryItemSlot[posX + x, posY + y])
                    {
                        return false; // Multiple different overlapping items found
                    }
                }
            }
        }
        return true; // No overlaps found
    }

    //METHOD: Check if given slot position is within the grid boundaries
    private bool IsWithinGrid(int slotX, int slotY)
    {
        return slotX >= 0 && slotX < gridSizeWidth && slotY >= 0 && slotY < gridSizeHeight;
    }

    //METHOD: check if item fits within the grid boundaries
    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        // Check if the item fits within the grid boundaries
        return IsWithinGrid(posX, posY) && IsWithinGrid(posX + width - 1, posY + height - 1);
    }

    // METHOD: retrieve an item from a specific grid position
    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y]; // Return the item reference from the grid position
    }

    // METHOD: find available space to insert an item within the grid
    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridSizeHeight - itemToInsert.Height + 1; // Calculate available height for insertion
        int width = gridSizeWidth - itemToInsert.Width + 1; // Calculate available width for insertion

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Check if the space is available for insertion
                if (CheckAvailableSpace(x, y, itemToInsert.Width, itemToInsert.Height))
                {
                    // Check if the insertion exceeds grid boundaries
                    if (BoundaryCheck(x, y, itemToInsert.Width, itemToInsert.Height))
                    {
                        return new Vector2Int(x, y); // Return the available insertion space
                    }
                    else
                    {
                        Debug.Log("Item exceeds grid boundaries");
                        return null; // No available space due to boundary constraints
                    }
                }
            }
        }
        Debug.Log("No space available in the grid"); // Log if no space is available in the grid
        return null; // No available space for insertion
    }

    //METHOD: check if there's available space for insertion
    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    return false; // If any slot is occupied, return false
                }
            }
        }

        return true; // If all slots are available, return true
    }
}
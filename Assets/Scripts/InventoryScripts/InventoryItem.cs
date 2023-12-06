using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;

    public int Height
    {
        get => rotated ? itemData.width : itemData.height;
    }

    public int Width
    {
        get => rotated ? itemData.height : itemData.width;
    }

    public int onGridPositionX;
    public int onGridPositionY;

    public bool rotated = false;

    internal void Rotate()
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.Rotate(new Vector3(0, 0, -90f));
    }

    internal void Set(ItemData itemData)
    {
        if (itemData == null)
        {
            Debug.LogError("ItemData is null");
            return;
        }

        this.itemData = itemData;

        Image itemImage = GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = this.itemData.itemIcon;
        }
        else
        {
            Debug.LogError("Image component not found");
        }

        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 size = new Vector2(Width * ItemGrid.TileSizeWidth, Height * ItemGrid.TileSizeHeight);
            rectTransform.sizeDelta = size;
        }
        else
        {
            Debug.LogError("RectTransform component not found");
        }
    }
}
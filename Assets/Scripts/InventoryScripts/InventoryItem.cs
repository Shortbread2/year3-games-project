using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    // used coroutine with waitForSecondsRealtine cos the game time scale with inventory open is set to 0 so had to use realtime to rotate object when time was stopped
    internal void Rotate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rotated = !rotated;
        StartCoroutine(WaitWithRealtime(rectTransform));
    }

    IEnumerator WaitWithRealtime(RectTransform rectTransform)
    {
        for (int i = 0; i < 90; i++){
            yield return new WaitForSecondsRealtime(0.001f);
            rectTransform.Rotate(new Vector3(0, 0, -1f));
        }
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
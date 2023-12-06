using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;

    public void ToggleHighlight(bool show)
    {
        if (highlighter != null)
        {
            highlighter.gameObject.SetActive(show);
        }
        else
        {
            Debug.LogError("Highlighter RectTransform is missing");
        }
    }

    public void ResizeHighlighter(InventoryItem targetItem)
    {
        if (highlighter != null && targetItem != null)
        {
            Vector2 size = new Vector2(
                targetItem.Width * ItemGrid.TileSizeWidth,
                targetItem.Height * ItemGrid.TileSizeHeight
            );
            highlighter.sizeDelta = size;
        }
        else
        {
            Debug.LogError("Highlighter RectTransform or targetItem is null");
        }
    }

    public void SetHighlighterPosition(ItemGrid targetGrid, InventoryItem targetItem)
    {
        if (targetGrid != null && targetItem != null && highlighter != null)
        {
            Vector2 position = targetGrid.CalculatePositionOnGrid(targetItem, targetItem.onGridPositionX, targetItem.onGridPositionY);
            highlighter.localPosition = position;
        }
        else
        {
            Debug.LogError("TargetGrid, TargetItem, or Highlighter RectTransform is null");
        }
    }

    public void SetHighlighterPositionFromGridAndItem(ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY)
    {
        if (targetGrid != null && targetItem != null && highlighter != null)
        {
            Vector2 position = targetGrid.CalculatePositionOnGrid(targetItem, posX, posY);
            highlighter.localPosition = position;
        }
        else
        {
            Debug.LogError("TargetGrid, TargetItem, or Highlighter RectTransform is null");
        }
    }

    public void SetHighlighterParent(ItemGrid targetGrid)
    {
        if (targetGrid != null && highlighter != null)
        {
            highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
        }
        else
        {
            Debug.LogError("TargetGrid or Highlighter RectTransform is null");
        }
    }
}

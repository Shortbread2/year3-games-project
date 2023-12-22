using UnityEngine;

public class IDCardPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryController.ItemType itemType = InventoryController.ItemType.IDCard;

            PlayerCollection collectiblesManager = collision.gameObject.GetComponent<PlayerCollection>();

            if (collectiblesManager != null)
            {
                collectiblesManager.CollectItem(itemType);
                Destroy(gameObject);
            }
        }
    }
}

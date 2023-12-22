using UnityEngine;

public class GemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryController.ItemType itemType = InventoryController.ItemType.Gem;

            PlayerCollection collectiblesManager = collision.gameObject.GetComponent<PlayerCollection>();

            if (collectiblesManager != null)
            {
                collectiblesManager.CollectItem(itemType);
                Destroy(gameObject);
            }
        }
    }
}

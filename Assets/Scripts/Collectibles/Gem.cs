using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCollection collectiblesManager = collision.gameObject.GetComponent<PlayerCollection>();

            if (collectiblesManager != null)
            {
                collectiblesManager.CollectItem("Gem");
                Destroy(gameObject);
            }
        }
    }
}

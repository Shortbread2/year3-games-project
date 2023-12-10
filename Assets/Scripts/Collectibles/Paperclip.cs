using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paperclip : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCollection collectiblesManager = collision.gameObject.GetComponent<PlayerCollection>();

            if (collectiblesManager != null)
            {
                collectiblesManager.CollectItem("Paperclip");
                Destroy(gameObject);
            }
        }
    }
<<<<<<< Updated upstream
}
=======
<<<<<<< HEAD
}
=======
}
>>>>>>> 034c6289673ab57ce8cce2a35f6c628439b4e428
>>>>>>> Stashed changes

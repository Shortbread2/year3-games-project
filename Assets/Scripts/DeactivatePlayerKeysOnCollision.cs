using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePlayerKeysOnCollision : MonoBehaviour
{

    public GameObject shopKeeperCollider;
    public playerMovement PlayerMove; // Reference to the script you want to deactivate
    public PlayerCollection PlayerCollection;

    public GameObject dialogue;


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject == shopKeeperCollider)
        {
            int gemCount = 0;
            if (PlayerCollection.collectiblesDictionary.TryGetValue("Gem", out gemCount))
            {
                if (gemCount < 5)
                {
                    Debug.Log("Not enough gems to trade");
                    dialogue.SetActive(true);
                }
                else
                {
                    // Player has enough gems
                    PlayerCollection.collectiblesDictionary["Gem"] -= 5;
                    PlayerMove.enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == shopKeeperCollider)
        {
            dialogue.SetActive(false);
        }
    }
}

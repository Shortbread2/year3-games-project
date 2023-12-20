using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePlayerKeysOnCollision : MonoBehaviour
{

    public GameObject shopKeeperCollider;
    public GameObject levelEntry;
    public playerMovement PlayerMove; // Reference to the script you want to deactivate
    public PlayerCollection PlayerCollection;

    public GameObject shopDialogue;

    public GameObject levelEntryDialogue;
    public GameObject miningEntry;
    public GameObject miningEntryDialogue;


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

                    shopDialogue.SetActive(true);
                }
                else
                {
                    // Player has enough gems
                    PlayerCollection.collectiblesDictionary["Gem"] -= 5;
                    PlayerMove.enabled = false;
                    PlayerMove.GetComponent<Animator>().enabled = false;
                }
            }
        }

        if (other.gameObject == levelEntry)
        {
            levelEntryDialogue.SetActive(true);
        }

        if (other.gameObject == miningEntry)
        {
            miningEntryDialogue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == shopKeeperCollider)
        {
            shopDialogue.SetActive(false);
        }

        if (other.gameObject == levelEntry)
        {
            levelEntryDialogue.SetActive(false);
        }

        if (other.gameObject == miningEntry)
        {
            miningEntryDialogue.SetActive(false);
        }
    }
}

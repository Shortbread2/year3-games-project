using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePlayerKeysOnCollision : MonoBehaviour
{

    public GameObject shopKeeperCollider;
    public playerMovement PlayerMove; // Reference to the script you want to deactivate


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject == shopKeeperCollider) //TODO && collectiblesDictionary["Gem"] >= 10
        {
            // Deactivate the script
            PlayerMove.isScriptActive = false;
        }
    }
}

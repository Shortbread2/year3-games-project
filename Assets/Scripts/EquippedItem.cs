using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    public GameObject weaponPrefab; // Reference to the weapon prefab to be instantiated
    private GameObject currentWeapon; // Reference to the currently equipped weapon
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void EquipWeapon()
    {
        if (player != null && weaponPrefab != null && playerItemLoc.childCount <= 0)
        {
            // Instantiate the weapon and attach it to the player
            currentWeapon = Instantiate(weaponPrefab, player);
            currentWeapon.transform.localPosition = Vector3.zero; // Set the position relative to the player

            // Adjust the sorting layer or order in layer for the weapon sprite renderer
            // Assuming the weapon has a SpriteRenderer component
            SpriteRenderer weaponRenderer = currentWeapon.GetComponent<SpriteRenderer>();
            if (weaponRenderer != null)
            {
                // You might need to experiment with the sorting order to get the desired effect
                weaponRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
        }
    }

    void Update()
    {
        // Continuously update the weapon position to stay attached to the player
        if (player != null && currentWeapon != null)
        {
            currentWeapon.transform.position = player.position;
        }
    }
}

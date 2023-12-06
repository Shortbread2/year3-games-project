using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    private Inventory inventory;
    public int currentSlot;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[currentSlot] = false;
        }
    }
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            child.GetComponent<SpawnWeapon>().SpawnDroppedItem();
            // if item is equipped get rid of that as well
            GameObject.Destroy(child.gameObject);
        }
    }
}

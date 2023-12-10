using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{

    private Inventory inventory;
    public int currentSlot;
    private Transform player;
    private Transform playerItemLoc;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < player.childCount; i++)
        {
            GameObject child = player.GetChild(i).gameObject;
            //Do something with child
            if (child.name == "item location"){
                playerItemLoc = child.transform;
            }
        }
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
            Debug.Log(child.name.Split(" ")[1]);
            if (playerItemLoc.childCount > 0){
                Debug.Log(playerItemLoc.GetChild(0).gameObject.name.Split("(")[1].Split(")")[0]);
                if (child.name.Split(" ")[1] == playerItemLoc.GetChild(0).gameObject.name.Split("(")[1].Split(")")[0]){
                    Destroy(playerItemLoc.GetChild(0).gameObject);
                }
            }
            GameObject.Destroy(child.gameObject);
        }
    }
}

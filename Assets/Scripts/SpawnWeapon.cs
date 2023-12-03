using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        if (player != null && item != null)
        {
            Vector3 playerPos = player.position;
            // Smaller offset for smaller tiles
            Vector3 spawnOffset = new Vector3(0f, 0.2f, 0f); // Adjust as needed
            Vector3 spawnPosition = playerPos + spawnOffset;

            Instantiate(item, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player or item reference missing!");
        }
    }
}
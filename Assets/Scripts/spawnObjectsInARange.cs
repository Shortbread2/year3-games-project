using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class spawnObjectsInARange : MonoBehaviour
{
    // the radius is the radius at which the enemies will spawn
    public float radius = 0.3f;
    // the offsetRadius is the radius at which the enemies wont spawn
    public float offsetRadius = 0f;
    public List<GameObject> entitysToSpawn = new List<GameObject>();
    [SerializeField]
    private List<GameObject> objectsInSpawnArea = new List<GameObject>();
    public GameObject parentObjctOfNewEntity;
    public float minSpawnRate = 3f;
    public float maxSpawnRate = 30f;
    [SerializeField]
    private float spawnRate;
    public float newEntityZValue = 0;
    private float lastAction;


    void Start()
    {
        spawnRate = Random.Range(3f, 30f);
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), radius);
        Handles.color = Color.green;
        Handles.DrawWireDisc(this.transform.position, new Vector3(0, 0, 1), offsetRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAction > spawnRate && objectsInSpawnArea.Count == 0){
            GameObject newEntity = Instantiate(entitysToSpawn[Random.Range(0,entitysToSpawn.Count)], this.transform.position + new Vector3(Random.Range(offsetRadius, radius),Random.Range(offsetRadius, radius), newEntityZValue), Quaternion.identity);
            newEntity.transform.parent = parentObjctOfNewEntity.transform;
            lastAction = Time.time;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }
    // i should have used this alot more
    private void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = radius;

    }

    private void OnTriggerEnter2D(Collider2D other){
        if (!objectsInSpawnArea.Contains(other.gameObject)){
            objectsInSpawnArea.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        objectsInSpawnArea.Remove(other.gameObject);            
    }
}

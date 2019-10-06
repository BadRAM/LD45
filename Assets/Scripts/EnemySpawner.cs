using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float startTime;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnHeight;
    [SerializeField] private List<GameObject> EnemyTypes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void FixedUpdate()
    {
        
    }

    public void Spawn(GameObject prefab)
    {
        Vector2 spot = spawnRadius * Random.insideUnitCircle;
        Vector3 pos = transform.position + new Vector3(spot.x, Random.Range(0, spawnHeight), spot.y);
        Instantiate(prefab, pos, Quaternion.identity);
    }
    
}

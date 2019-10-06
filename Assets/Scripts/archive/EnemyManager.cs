using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> Waves; // Waves to spawn, Float = Time to spawn
    private float _startTime;
    [SerializeField] private float SpawnRadius;
    [SerializeField] private float SpawnHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        
        GameInfo.Enemies = new List<Enemy>();
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            GameInfo.Enemies.Append(e);
        }
    }

    private void FixedUpdate()
    {
        if (Waves.Count > 0 && Waves[0].Time < Time.time - _startTime)
        {
            for (int i = 0; i < Waves[0].Number; i++)
            {
                Vector2 spot = SpawnRadius * Random.insideUnitCircle;
                Vector3 pos = transform.position + new Vector3(spot.x, Random.Range(0, SpawnHeight), spot.y);
                Instantiate(Waves[0].EnemyType, pos, Quaternion.identity);
            }

            Waves.RemoveAt(0);
        }
    }
}

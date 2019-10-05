using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        EnemyList.Enemies = new List<Enemy>();
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            EnemyList.Enemies.Append(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

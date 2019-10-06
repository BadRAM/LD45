using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAICluster : EnemyAI
{
    // this ai will stand and shoot if there are requiredFlock other enemies within nearThresh units of it.
    // otherwise it will path towards the second nearest enemy.
    
    [SerializeField] private float nearThresh = 5;
    [SerializeField] private int requiredFlock = 3;
    
    public override void AiBehavior()
    {
        List<KeyValuePair<Enemy, float>> enemyDistances =new List<KeyValuePair<Enemy, float>>();
        foreach (Enemy e in GameInfo.Enemies)
        {
            enemyDistances.Add(new KeyValuePair<Enemy, float>(e, Vector3.Distance(e.transform.position, transform.position)));
        }

        var sortedDict = from entry in enemyDistances orderby entry.Value ascending select entry;
        
        if (enemyDistances[requiredFlock].Value < nearThresh)
        {
            _fire();
        }
    }
}

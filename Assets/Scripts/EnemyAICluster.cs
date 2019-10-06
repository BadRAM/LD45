using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAICluster : EnemyAI
{
    // this ai will stand and shoot if there are requiredFlock other enemies within nearThresh units of it.
    // otherwise it will path towards the second nearest enemy.
    
    [SerializeField] private float nearThresh = 5;
    [SerializeField] private int requiredFlock = 2;
    private Transform _targetAlly;
    
    public override void AiBehavior()
    {
        if (_targetAlly == null || Vector3.Distance(_targetAlly.position, transform.position) < nearThresh
            || Vector3.Distance(transform.position, GameInfo.Player.transform.position) < nearThresh)
        {
            List<KeyValuePair<Enemy, float>> enemyDistances =new List<KeyValuePair<Enemy, float>>();
            foreach (Enemy e in GameInfo.Enemies)
            {
                enemyDistances.Add(new KeyValuePair<Enemy, float>(e, Vector3.Distance(e.transform.position, transform.position)));
            }
        
            enemyDistances.Remove(new KeyValuePair<Enemy, float>(GetComponent<Enemy>(), 0f));
            
            enemyDistances = enemyDistances.OrderBy(x => x.Value).ToList();

            if (enemyDistances.Count >= requiredFlock)
            {
                _agent.SetDestination(transform.position);
                _fire();
            }
            else if (enemyDistances[requiredFlock - 1].Value < nearThresh)
            {
                _agent.SetDestination(transform.position);
                _fire();
            }
            else
            {
                _targetAlly = enemyDistances[requiredFlock - 1].Key.transform;
            }
        }
        else
        {
            _agent.SetDestination(_targetAlly.position);
        }
    }
}

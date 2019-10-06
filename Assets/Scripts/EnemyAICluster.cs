﻿using System.Collections;
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
    private List<KeyValuePair<Enemy, float>> enemyDistances = new List<KeyValuePair<Enemy, float>>();

    public override void AiBehavior()
    {
        if (GameInfo.Enemies.Count <= requiredFlock || 
            Vector3.Distance(transform.position, _playerTransform.transform.position) < nearThresh)
        {
            runAway();
        }
        else if (_targetAlly == null || Vector3.Distance(_targetAlly.position, transform.position) < nearThresh)
        {
            enemyDistances = scanAllies();
            
            if (enemyDistances[requiredFlock - 1].Value < nearThresh)
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

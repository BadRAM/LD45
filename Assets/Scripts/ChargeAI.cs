using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAI : EnemyAI
{
    public override void AiBehavior()
    {
        _agent.SetDestination(_playerTransform.position);
        if (Vector3.Distance(_playerTransform.position, transform.position) < 1.5f)
        {
            _fire();
        }
    }
}

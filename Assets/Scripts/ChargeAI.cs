using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAI : EnemyAI
{
    private float _detoTimer;
    
    public override void AiBehavior()
    {
        if (Vector3.Distance(_playerTransform.position, transform.position) < 2f)
        {
            _agent.SetDestination(transform.position);
            _detoTimer += Time.deltaTime;
            if (_detoTimer > 0.5)
            {
                _fire();
            }
        }
        else
        {
            _agent.SetDestination(_playerTransform.position);
            _detoTimer = 0;
        }
    }
}

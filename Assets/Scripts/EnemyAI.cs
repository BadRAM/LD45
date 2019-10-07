using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    //[SerializeField] private float fireCooldown = 1;
    private float _fireHeat;
    protected Transform _playerTransform;
    protected EnemyWeapon _weapon;
    protected NavMeshAgent _agent;
    //protected string _state;

    // default EnemyAI moves towards the player and shoots at the same time. stops within 5m.
    
    protected virtual void Start()
    {
        _playerTransform = GameInfo.Player.transform;
        _weapon = GetComponent<EnemyWeapon>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        
    }

    public virtual void AiBehavior()
    {
        if (Vector3.Distance(_playerTransform.position, transform.position) < 5)
        {
            _agent.SetDestination(transform.position);
        }
        
        if (_fireHeat == 0)
        {
            _fire();
            _fireHeat = 1;
            _agent.SetDestination(_playerTransform.position);
        }

        _fireHeat = Mathf.Max(0, _fireHeat - Time.deltaTime);
    }

    protected bool _fire()
    {
        return _weapon.Fire(_playerTransform.position);
    }
    
    protected void runAway()
    {
        Vector3 targetPos = (transform.position - _playerTransform.transform.position).normalized * 2;
        
        _agent.SetDestination(transform.position + targetPos);
    }
    
    protected List<KeyValuePair<Enemy, float>> scanAllies()
    {
        List<KeyValuePair<Enemy, float>> enemyDistances = new List<KeyValuePair<Enemy, float>>();
        
        foreach (Enemy e in GameInfo.Enemies)
        {
            enemyDistances.Add(new KeyValuePair<Enemy, float>(e, Vector3.Distance(e.transform.position, transform.position)));
        }
        
        enemyDistances.Remove(new KeyValuePair<Enemy, float>(GetComponent<Enemy>(), 0f));
            
        return enemyDistances.OrderBy(x => x.Value).ToList();
    }
    
    public virtual int getFlip()
    {
        return 1;
    }
}

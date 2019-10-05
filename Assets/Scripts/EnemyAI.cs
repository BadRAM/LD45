using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float fireCooldown = 1;
    private float _fireHeat;
    private Transform _playerTransform;
    private EnemyWeapon _weapon;
    private NavMeshAgent _agent;
    private string _state;

    // default EnemyAI moves towards the player and shoots at the same time. stops within 5m.
    
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _weapon = GetComponent<EnemyWeapon>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        
    }

    public void Behavior()
    {
        if (Vector3.Distance(_playerTransform.position, transform.position) < 5)
        {
            _agent.SetDestination(transform.position);
        }
        
        if (_fireHeat == 0)
        {
            _fire();
            _fireHeat = fireCooldown;
            _agent.SetDestination(_playerTransform.position);
        }

        _fireHeat = Mathf.Max(0, _fireHeat - Time.deltaTime);
    }

    private void _fire()
    {
        _weapon.Fire(_playerTransform.position);
    }
}

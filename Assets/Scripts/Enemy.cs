﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private float Health;
    private NavMeshAgent _agent;
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private EnemyAI _AI;
    private Vector3 _playerPos;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        //_agent.SetDestination(_playerPos);
    }

    public void Hurt(float damageTaken)
    {
        Health -= damageTaken;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        //activate death particles, disable functional bits.
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private float Health;
    private EnemyWeapon _weapon;
    private EnemyAI _AI;
    private Transform _playerTransform;
    private NavMeshAgent _agent;
    [SerializeField]private GameObject disableOnDeath;
    [SerializeField]private GameObject enableOnDeath;
    private bool falling = true;

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponent<EnemyWeapon>();
        _AI = GetComponent<EnemyAI>();
        _agent = GetComponent<NavMeshAgent>();
        //_agent.enabled = false;

        if (!GameInfo.Enemies.Contains(this))
        {
            GameInfo.Enemies.Add(this);
        }
    }

    private void FixedUpdate()
    {
        if (falling)
        {
            transform.position += Vector3.down * Time.deltaTime * 5;
            if (transform.position.y < 1)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                //Vector3 pos = transform.position;
                _agent.enabled = true;
                //_agent.Warp(pos);
                falling = false;
            }
        }
        else if (Health > 0)
        {
            _AI.AiBehavior();
        }
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
        GameInfo.Enemies.Remove(this);
        disableOnDeath.SetActive(false);
        enableOnDeath.SetActive(true);
    }
}

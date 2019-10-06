using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private float Health;
    [SerializeField] private float DeathDuration;
    private float _timeOfDeath;
    private EnemyWeapon _weapon;
    private EnemyAI _AI;
    private Transform _playerTransform;
    private NavMeshAgent _agent;
    [SerializeField]private GameObject disableOnDeath;
    [SerializeField]private GameObject enableOnDeath;
    private bool _falling = true;
    

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponent<EnemyWeapon>();
        _AI = GetComponent<EnemyAI>();
        _agent = GetComponent<NavMeshAgent>();
        //_agent.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_falling)
        {
            transform.position += Vector3.down * Time.deltaTime * 5;
            if (transform.position.y < 1)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                _agent.enabled = true;
                _falling = false;
                
                if (!GameInfo.Enemies.Contains(GetComponent<Enemy>()))
                {
                    GameInfo.Enemies.Add(GetComponent<Enemy>());
                }
            }
        }
        else if (Health > 0)
        {
            _AI.AiBehavior();
        }
        else if (Time.time - _timeOfDeath > DeathDuration)
        {
            Destroy(gameObject);
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

    public void Die()
    {
        Health = 0;
        _timeOfDeath = Time.time;
        GameInfo.Enemies.Remove(this);
        disableOnDeath.SetActive(false);
        enableOnDeath.SetActive(true);
    }
}

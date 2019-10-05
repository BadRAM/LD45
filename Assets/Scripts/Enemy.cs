using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{

    [SerializeField] private float Health;
    private EnemyWeapon _weapon;
    private EnemyAI _AI;
    private Transform _playerTransform;
    [SerializeField]private GameObject disableOnDeath;
    [SerializeField]private GameObject enableOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        _weapon = GetComponent<EnemyWeapon>();
        _AI = GetComponent<EnemyAI>();
    }

    private void FixedUpdate()
    {
        if (Health > 0)
        {
            _AI.Behavior();
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
        disableOnDeath.SetActive(false);
        enableOnDeath.SetActive(true);
    }
}

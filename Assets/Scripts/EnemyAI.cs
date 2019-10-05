using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float fireCooldown = 1;
    private float _fireHeat;
    private Transform _playerTransform;
    [SerializeField] private GameObject Projectile; 
    
    // default EnemyAI moves towards the player
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (_fireHeat == 0)
        {
            _fire();
            _fireHeat = fireCooldown;
        }

        _fireHeat = Mathf.Max(0, _fireHeat - Time.deltaTime);
    }

    private void _fire()
    {
        Instantiate(Projectile, transform.position,
            Quaternion.LookRotation(_playerTransform.position - transform.position, transform.up));
        Debug.Log("Firing");
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;
    private Rigidbody _rigidbody;
    [SerializeField] private float Duration;
    [SerializeField] private bool Persist;
    private float _startTime;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        if (Time.time - _startTime > Duration)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        
        if (other.TryGetComponent(out PlayerCharacter p))
        {
            p.Hurt(Damage);
        }
        
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<Enemy>().Hurt(Damage);
        }

        if (!Persist)
        {
            Destroy(gameObject);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * 0.9f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;
    private Rigidbody _rigidbody;
    [SerializeField] private float Duration;
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

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent(out PlayerCharacter p))
        {
            p.Hurt(Damage);
        }
        

        if (other.transform.parent.TryGetComponent(out Enemy e))
        {
            e.Hurt(Damage);
        }
        
        Destroy(gameObject);
    }
}

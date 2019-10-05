using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Damage;
    private Rigidbody _rigidbody;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.TryGetComponent(out Player p))
        {
            Player.Damage(Damage);
        }
        */

        if (other.TryGetComponent(out Enemy e))
        {
            e.Damage(Damage);
        }
    }
}

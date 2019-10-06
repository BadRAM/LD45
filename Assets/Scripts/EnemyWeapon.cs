using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;
    private float heat;

    private void FixedUpdate()
    {
        heat = Mathf.Max(0, heat - Time.deltaTime);
    }

    
    public virtual bool Fire(Vector3 target) // returns true if shot fired successfully.
    {
        if (heat == 0)
        {
            Instantiate(projectile, transform.position,
                Quaternion.LookRotation(target - transform.position, transform.up));
            heat = cooldown;
            return true;
        }
        return false;
    }
}

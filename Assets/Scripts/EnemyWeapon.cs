using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    public void Fire(Vector3 target)
    {
        Instantiate(projectile, transform.position,
            Quaternion.LookRotation(target - transform.position, transform.up));
    }
}

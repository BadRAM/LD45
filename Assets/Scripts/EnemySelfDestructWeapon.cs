using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelfDestructWeapon : EnemyWeapon
{
    [SerializeField] private float damage;

    public override void Fire(Vector3 target)
    {
        if (Vector3.Distance(GameInfo.Player.transform.position, transform.position) < 2)
        {
            GameInfo.Player.Hurt(damage);
            GetComponent<Enemy>().Die();
        }
    }
}

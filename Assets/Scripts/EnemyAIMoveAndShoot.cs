using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// attempts to flank the player while shooting and maintaining distance
public class EnemyAIMoveAndShoot : EnemyAI
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;
    private int _flip = 1;

    protected override void Start()
    {
        base.Start();
        _flip = Random.Range(0, 1);
        if (_flip == 0)
        {
            _flip = -1;
        }
    }

    public override void AiBehavior()
    {
        /*
        int tally = 0;
        foreach (var e in GameInfo.Enemies)
        {
            tally += e.GetComponent<EnemyAI>().getFlip();
        }

        if (tally < -1 && _flip == -1)
        {
            _flip = 1;
        }
        else if (tally > 1 && _flip == 1)
        {
            _flip = -1;
        }
        */

        Vector3 pdir = (_playerTransform.position - transform.position).normalized; // direction to player.

        Vector3 moveDir = Vector3.Cross(pdir, Vector3.up * _flip);

        if (Vector3.Distance(_playerTransform.position, transform.position) > maxDistance)
        {
            moveDir += pdir;
        }
        else if (Vector3.Distance(_playerTransform.position, transform.position) < minDistance)
        {
            moveDir += pdir * -1;
        }


        _agent.SetDestination(transform.position + moveDir);

        _fire();
    }

    public override int getFlip()
    {
        return _flip;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// an enemy AI which alternates between firing a random number of shots and moving toward the player in a random direction for a random interval
// will retreat if move phase entered while player is in fearthreshold.
public class EnemyAIRandomApproach : EnemyAI
{
    [SerializeField] private int maxShots = 3;
    [SerializeField] private int minShots = 1;
    [SerializeField] private float maxMoveTime = 3;
    [SerializeField] private float minMoveTime = 1;
    [SerializeField] private float fearThreshold = 3;
    private int _shotqueue;
    private float _moveTime;
    private bool _moving;
    private bool _retreating;
    private Vector3 _moveDir;


    public override void AiBehavior()
    {
        if (_shotqueue > 0)
        {
            if (_fire())
            {
                _shotqueue -= 1;
            }
        }
        else if (!_moving)
        {
            // invert the reflect check if player is too close
            bool fear = Vector3.Distance(_playerTransform.position, transform.position) < fearThreshold;

            Vector2 dir = Random.insideUnitCircle;
            Vector3 dir3 = new Vector3(dir.x, 0, dir.y);
            Vector3 pdir = (_playerTransform.position - transform.position).normalized; // direction to player.
            if (Vector3.Dot(dir3, pdir) < 0 ^ fear)
            {
                dir3 = Vector3.Reflect(dir3, Vector3.Cross(pdir, Vector3.up));
            }
            _moveDir = dir3;
            _moveTime = Random.Range(minMoveTime, maxMoveTime);
            _moving = true;
        }
        else
        {
            if (_moveTime == 0)
            {
                _shotqueue = Random.Range(minShots, maxShots);
                _moving = false;
            }
            else
            {
                _moveTime = Mathf.Max(0, _moveTime - Time.deltaTime);
            }
            _agent.SetDestination(transform.position + _moveDir);
        }
    }
}

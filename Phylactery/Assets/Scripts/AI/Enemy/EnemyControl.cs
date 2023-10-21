using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : AIControl
{
    protected float _playerDetectionDistance;
    protected float _playerDetectionAngle;
    protected float _attackRange;
    protected Vector2 _forwardVec;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // Waiting for Amy to finish implementing the player control
    public virtual bool IsInPlayerRange()
    {
        Vector2 playerPos = new Vector2(0.0f, 0.0f);
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);

        if (Vector2.Distance(playerPos, enemyPos) > _playerDetectionDistance)
        {
            return false;
        }

        if (Vector2.Angle(_forwardVec, playerPos - enemyPos) > _playerDetectionAngle)
        {
            return false;
        }

        return true;
    }
}

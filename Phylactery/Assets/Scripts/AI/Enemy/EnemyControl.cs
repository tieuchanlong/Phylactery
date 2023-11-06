using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : AIControl
{
    [SerializeField]
    protected float _playerDetectionDistance;
    [SerializeField]
    protected float _playerDetectionAngle;
    [SerializeField]
    protected float _attackRange;
    protected Vector2 _forwardVec;
    protected BasePlayerMovement _player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _player = FindObjectOfType<BasePlayerMovement>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // Waiting for Amy to finish implementing the player control
    public virtual bool IsInPlayerRange()
    {
        Vector2 playerPos = _player.transform.position;
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

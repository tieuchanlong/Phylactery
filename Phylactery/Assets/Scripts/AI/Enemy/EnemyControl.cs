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
    [SerializeField]
    protected float _attackDamage = 1.0f;
    protected Vector2 _forwardVec;

    protected bool _detectedPlayer = false;
    public bool DetectedPlayer
    {
        get
        {
            return _detectedPlayer;
        }
    }

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
        Vector2 playerPos = _player.transform.position;
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);

        if (Vector2.Distance(playerPos, enemyPos) > _playerDetectionDistance)
        {
            _detectedPlayer = false;
            return false;
        }

        if (Vector2.Angle(_forwardVec, playerPos - enemyPos) > _playerDetectionAngle)
        {
            _detectedPlayer = false;
            return false;
        }

        _detectedPlayer = true;
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "StonePebble")
        {
            ProjectTileControl projectTile = collision.GetComponent<ProjectTileControl>();
            TakeDamage(projectTile.ProjectTileDamage);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "StonePebble")
        {
            ProjectTileControl projectTile = collision.GetComponent<ProjectTileControl>();
            TakeDamage(projectTile.ProjectTileDamage);
            Destroy(collision.gameObject);
        }
    }

    public override void Die()
    {
        _detectedPlayer = false;
        base.Die();
    }
}

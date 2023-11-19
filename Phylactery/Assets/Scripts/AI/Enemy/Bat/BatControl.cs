using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControl : EnemyControl
{
    private bool _doAttackAnimation = false;

    protected override void Start()
    {
        runDirections = new string[] { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
        _aiRootNode = new BatNode(this, null);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override bool IsInPlayerRange()
    {
        Vector2 playerPos = _player.transform.position;
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);

        if (Vector2.Distance(playerPos, enemyPos) > _playerDetectionDistance)
        {
            _detectedPlayer = false;
            return false;
        }

        _detectedPlayer = true;
        return true;
    }

    public bool ChasePlayer()
    {
        _destination = _player.GetComponent<DestinationLocationControl>();

        if (ReachedDestination())
        {
            return true;
        }

        return false;
    }

    public bool DoAttackAction()
    {
        if (!_doAttackAnimation)
        {
            StartCoroutine(DoAttackAnimation());
            return true;
        }

        return false;
    }

    IEnumerator DoAttackAnimation()
    {
        // Damage player every 2s
        _doAttackAnimation = true;

        // Spawn projectile
        GameObject projectTilePrefab = Resources.Load("Borrowed/Prefabs/Miscs/ProjectTile") as GameObject;
        GameObject projectTile = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
        projectTile.GetComponent<ProjectTileControl>().ProjectTileDirection = (_player.transform.position - transform.position).normalized;

        yield return new WaitForSeconds(4);

        _doAttackAnimation = false;
    }
}

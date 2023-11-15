using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : EnemyControl
{
    public static string[] attackDirections = { "Attack N", "Attack NW", "Attack W", "Attack SW", "Attack S", "Attack SE", "Attack E", "Attack NE" };
    private bool _doAttackAnimation = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        runDirections = new string[] { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
        _aiRootNode = new ZombieNode(this, null);
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
            return false;
        }

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
        _charRenderer.SetDirection(_headingDirection, attackDirections);
        _player.TakeDamage(_attackDamage);
        yield return new WaitForSeconds(2);

        _doAttackAnimation = false;
    }
}

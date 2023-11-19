using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenControl : EnemyControl
{
    private bool _doAttackAnimation = false;

    private int _attackPhase = 0;

    private CoachmenWagonWheelControl _wagonWheel;

    public static string[] attackDirections = { "Attack N", "Attack NW", "Attack W", "Attack SW", "Attack S", "Attack SE", "Attack E", "Attack NE" };
    public static string[] castDirections = { "Cast N", "Cast NW", "Cast W", "Cast SW", "Cast S", "Cast SE", "Cast E", "Cast NE" };

    protected override void Start()
    {
        runDirections = new string[] { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
        _aiRootNode = new CoachmenNode(this, null);
        _wagonWheel = GetComponentInChildren<CoachmenWagonWheelControl>();
        _wagonWheel.gameObject.SetActive(false);
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

    public bool IsAttackingPlayer()
    {
        return _doAttackAnimation;
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
            if (_attackPhase == 0)
            {
                StartCoroutine(DoWhipAttackAnimation());
            }
            else
            {
                StartCoroutine(DoWagonWheelAttackAnimation());
            }

            _attackPhase = (_attackPhase + 1) % 2;
            return true;
        }

        return false;
    }

    IEnumerator DoWhipAttackAnimation()
    {
        // Damage player every 2s
        _doAttackAnimation = true;

        // Play whip attack animation
        _charRenderer.SetDirection(new Vector2((_player.transform.position - transform.position).x, (_player.transform.position - transform.position).y), attackDirections);

        if (Vector3.Distance(_player.transform.position, transform.position) <= _attackRange)
        {
            _player.TakeDamage(1.0f);
        }

        yield return new WaitForSeconds(2);

        _charRenderer.SetDirection(new Vector2((_player.transform.position - transform.position).x, (_player.transform.position - transform.position).y), staticDirections);
        yield return new WaitForSeconds(2);

        _doAttackAnimation = false;
    }

    IEnumerator DoWagonWheelAttackAnimation()
    {
        // Damage player every 2s
        _doAttackAnimation = true;

        // Spawn projectile
        _wagonWheel.gameObject.SetActive(true);
        _wagonWheel.ProjectTileDirection = (_player.transform.position - transform.position).normalized;
        _wagonWheel.Destination = _player.transform.position;

        _charRenderer.SetDirection(new Vector2((_player.transform.position - transform.position).x, (_player.transform.position - transform.position).y), castDirections);

        yield return new WaitForSeconds(6);

        _wagonWheel.transform.position = transform.position;
        _wagonWheel.gameObject.SetActive(false);

        _charRenderer.SetDirection(new Vector2((_player.transform.position - transform.position).x, (_player.transform.position - transform.position).y), staticDirections);
        yield return new WaitForSeconds(2);

        _doAttackAnimation = false;
    }
}

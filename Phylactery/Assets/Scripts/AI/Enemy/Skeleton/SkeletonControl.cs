using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonControl : EnemyControl
{
    [SerializeField]
    private float _dashSpeed = 6.0f;

    [SerializeField]
    private float _dashStopDist = 0.1f;

    [SerializeField]
    private float _dashDamageRange = 0.1f;
    private bool _finishedDashDamage = false;

    private bool _doDashAnimation;
    private float _prevDashSpeed;
    private bool _delayAttack = false;
    private float _prevStopDist;


    // Start is called before the first frame update
    protected override void Start()
    {
        _aiRootNode = new SkeletonNode(this, null);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public bool ChasePlayer()
    {
        // Don't chase player when inside player range
        if (IsInPlayerRange() || _delayAttack)
        {
            return true;
        }

        _destination = _player.GetComponent<DestinationLocationControl>();

        if (ReachedDestination())
        {
            return true;
        }

        return false;
    }

    public bool DoDashAction()
    {
        if (_delayAttack)
        {
            return false;
        }

        if (!_doDashAnimation)
        {
            // Play dash animation
            Vector3 dashDir = _player.transform.position - transform.position;
            dashDir = 1.5f * dashDir;
            GameObject dest = Resources.Load("Original/Prefabs/Miscs/Dest") as GameObject;
            GameObject destInst = Instantiate(dest, transform.position + dashDir, Quaternion.identity);
            _destination = destInst.GetComponent<DestinationLocationControl>();
            _prevDashSpeed = _rushSpeed;
            _rushSpeed = _dashSpeed;
            _prevStopDist = _stopDistance;
            _stopDistance = _dashStopDist;
            _doDashAnimation = true;
        }

        if (Vector3.Distance(_player.transform.position, transform.position) <= _dashDamageRange && !_finishedDashDamage)
        {
            _finishedDashDamage = true;
            _player.TakeDamage(1.0f);
        }

        if (!ReachedDestination())
        {
            MoveToDestination(true);
            return false;
        }

        _doDashAnimation = false;
        _rushSpeed = _prevDashSpeed;
        _stopDistance = _prevStopDist;
        Destroy(_destination.gameObject);

        if (!_delayAttack)
        {
            StartCoroutine(DoDelayAction());
        }
        return true;
    }

    IEnumerator DoDelayAction()
    {
        _delayAttack = true;
        _finishedDashDamage = false;
        PlayIdleAnimation();

        yield return new WaitForSeconds(3);

        _delayAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _destination = null;
        _doDashAnimation = false;
        StartCoroutine(DoDelayAction());
    }
}

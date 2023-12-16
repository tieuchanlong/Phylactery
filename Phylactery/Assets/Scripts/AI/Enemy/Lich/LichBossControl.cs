using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichBossControl : EnemyControl
{
    private enum AttackMode
    {
        TripleBlast,
        FromBeneath,
    }

    private int _attackPhase = 0;
    private AttackMode _attackMode = AttackMode.TripleBlast;
    private bool _doAttackAnimation = false;
    private bool _delayIdle = false;

    private BattleControl _battleControl;
    private Animator _animator;

    [SerializeField]
    private GameObject _endLevelTrigger;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    protected GameObject _zombiePrefab;

    [SerializeField]
    protected GameObject _skeletonPrefab;

    [SerializeField]
    protected GameObject _casketPrefab;

    [SerializeField]
    protected GameObject _coachmenPrefab;

    [SerializeField]
    protected GameObject _eyebatPrefab;

    [SerializeField]
    private List<Transform> _spawnPoints;

    [SerializeField]
    private AudioClip _tripleBlastCastSound;

    [SerializeField]
    private AudioClip _fromBeneathCastSound;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private bool _dying = false;

    protected override void Start()
    {
        _aiRootNode = new LichNode(this, null);
        _battleControl = FindObjectOfType<BattleControl>();
        _animator = GetComponent<Animator>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (IsInPlayerAttackRange(2.7f))
        {
            TakeDamage(1.0f);
        }

        int i = 0;
        while (i < _spawnedEnemies.Count)
        {
            if (_spawnedEnemies[i] == null)
            {
                _spawnedEnemies.RemoveAt(i);
                i--;
            }

            i++;
        }

        _invicible = _spawnedEnemies.Count > 0;
        _shield.SetActive(_invicible);

        base.Update();
    }

    public override void TakeDamage(float hpChangeAmount)
    {
        _audio.Stop();
        base.TakeDamage(hpChangeAmount);
    }

    private void SelectAttack()
    {
        if (_attackPhase <= 1)
        {
            _attackMode = (AttackMode)(Random.Range((int)AttackMode.TripleBlast, (int)AttackMode.TripleBlast + 1));
        }
        else if (_attackPhase >= 2)
        {
            _attackMode = (AttackMode)(Random.Range((int)AttackMode.TripleBlast, (int)AttackMode.FromBeneath + 1));
        }
    }

    public void DoAttackPhase1()
    {
        if (_hp/_maxHP <= 0.75f && _attackPhase == 0)
        {
            // Spawn 2 skeletons and 2 zombies
            _attackPhase = 1;
            StartCoroutine(SpawnPhase1());
        }

        if (!_doAttackAnimation && !_delayIdle)
        {
            SelectAttack();
            StartAttackAction();
        }
    }

    public void DoAttackPhase2()
    {
        if (_hp / _maxHP <= 0.5f && _attackPhase < 2)
        {
            // Spawn 2 caskets and 2 eyebats
            _attackPhase = 2;
            _battleControl.PlayBossPhase2();
            StartCoroutine(SpawnPhase2());
        }

        if (!_doAttackAnimation && !_delayIdle)
        {
            SelectAttack();
            StartAttackAction();
        }
    }

    public void DoAttackPhase3()
    {
        if (_hp / _maxHP <= 0.25f && _attackPhase < 3)
        {
            // Spawn 1 coachman and 2 eyebats
            _attackPhase = 3;
            StartCoroutine(SpawnPhase3());
        }

        if (!_doAttackAnimation && !_delayIdle)
        {
            SelectAttack();
            StartAttackAction();
        }
    }

    private void StartAttackAction()
    {
        _doAttackAnimation = true;

        if (_attackMode == AttackMode.TripleBlast)
        {
            // Do the triple blast animation
            StartCoroutine(DoTripleBlastAnimation());
        }
        else if (_attackMode == AttackMode.FromBeneath)
        {
            // Do the attack from beneath
            StartCoroutine(DoFromBeneathAttack());
        }
    }

    IEnumerator SpawnPhase1()
    {
        GameObject enemy1 = Instantiate(_skeletonPrefab, _spawnPoints[0].position, Quaternion.identity);
        GameObject enemy2 = Instantiate(_zombiePrefab, _spawnPoints[1].position, Quaternion.identity);
        GameObject enemy3 = Instantiate(_skeletonPrefab, _spawnPoints[2].position, Quaternion.identity);
        GameObject enemy4 = Instantiate(_zombiePrefab, _spawnPoints[3].position, Quaternion.identity);
        _spawnedEnemies.Add(enemy1);
        _spawnedEnemies.Add(enemy2);
        _spawnedEnemies.Add(enemy3);
        _spawnedEnemies.Add(enemy4);

        PhylacteryPlayerMovement _player = FindAnyObjectByType<PhylacteryPlayerMovement>();

        if (Vector3.Distance(_player.transform.position, transform.position) <= 3.5f)
        {
            Vector2 reflectDir = _player.transform.position - transform.position;
            reflectDir.Normalize();
            _player.GetComponent<Rigidbody2D>().AddForce(reflectDir * 60.0f);
        }

        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator SpawnPhase2()
    {
        GameObject enemy1 = Instantiate(_casketPrefab, _spawnPoints[0].position, Quaternion.identity);
        GameObject enemy2 = Instantiate(_eyebatPrefab, _spawnPoints[1].position, Quaternion.identity);
        GameObject enemy3 = Instantiate(_casketPrefab, _spawnPoints[2].position, Quaternion.identity);
        GameObject enemy4 = Instantiate(_eyebatPrefab, _spawnPoints[3].position, Quaternion.identity);
        _spawnedEnemies.Add(enemy1);
        _spawnedEnemies.Add(enemy2);
        _spawnedEnemies.Add(enemy3);
        _spawnedEnemies.Add(enemy4);

        PhylacteryPlayerMovement _player = FindAnyObjectByType<PhylacteryPlayerMovement>();

        if (Vector3.Distance(_player.transform.position, transform.position) <= 3.5f)
        {
            Vector2 reflectDir = _player.transform.position - transform.position;
            reflectDir.Normalize();
            _player.GetComponent<Rigidbody2D>().AddForce(reflectDir * 100.0f);
        }

        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator SpawnPhase3()
    {
        GameObject enemy1 = Instantiate(_coachmenPrefab, _spawnPoints[0].position, Quaternion.identity);
        GameObject enemy2 = Instantiate(_eyebatPrefab, _spawnPoints[1].position, Quaternion.identity);
        GameObject enemy3 = Instantiate(_eyebatPrefab, _spawnPoints[3].position, Quaternion.identity);
        _spawnedEnemies.Add(enemy1);
        _spawnedEnemies.Add(enemy2);
        _spawnedEnemies.Add(enemy3);

        if (Vector3.Distance(_player.transform.position, transform.position) <= 3.5f)
        {
            Vector2 reflectDir = _player.transform.position - transform.position;
            reflectDir.Normalize();
            _player.GetComponent<Rigidbody2D>().AddForce(reflectDir * 60.0f);
        }

        yield return new WaitForSeconds(2.0f);
    }

    public override void DoDamageAnimation()
    {
        
    }

    IEnumerator DoTripleBlastAnimation()
    {
        // Play some music to warn player first
        _audio.clip = _tripleBlastCastSound;
        _audio.volume = 0.05f;
        _audio.Play();
        _animator.SetBool("Attack", true);
        yield return new WaitForSeconds(1.0f);

        _doAttackAnimation = true;

        // Spawn projectile
        SpawnBossProjecTile();
        yield return new WaitForSeconds(0.1f);

        SpawnBossProjecTile();
        yield return new WaitForSeconds(0.1f);

        SpawnBossProjecTile();
        yield return new WaitForSeconds(0.1f);

        SpawnBossProjecTile();
        yield return new WaitForSeconds(0.1f);

        _doAttackAnimation = false;
        _delayIdle = true;

        _animator.SetBool("Attack", false);
        StartCoroutine(DelayAttack());
    }

    private void SpawnBossProjecTile()
    {
        Vector3 headingDir = (_player.transform.position - transform.position).normalized;

        if (_attackPhase == 3)
        {
            // Spawn projectile
            GameObject projectTilePrefab = Resources.Load("Borrowed/Prefabs/Miscs/BossProjectTile") as GameObject;
            GameObject projectTile = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile.GetComponent<ProjectTileControl>().ProjectTileDirection = headingDir;
            projectTile.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;

            // Spawn projectile
            GameObject projectTile1 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile1.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(-30, new Vector3(0, 0, 1)) * headingDir;
            projectTile1.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile1.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;

            // Spawn projectile
            GameObject projectTile2 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile2.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(30, new Vector3(0, 0, 1)) * headingDir;
            projectTile2.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile2.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;

            // Spawn extra 3 more directions
            // Spawn projectile
            GameObject projectTile3 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile3.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(-60, new Vector3(0, 0, 1)) * headingDir;
            projectTile3.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile3.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;

            // Spawn projectile
            GameObject projectTile4 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile4.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(60, new Vector3(0, 0, 1)) * headingDir;
            projectTile4.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile4.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;

            // Spawn projectile
            GameObject projectTile5 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile5.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * headingDir;
            projectTile5.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile5.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;

            // Spawn projectile
            GameObject projectTile6 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile6.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(90, new Vector3(0, 0, 1)) * headingDir;
            projectTile6.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile6.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;

            // Spawn projectile
            GameObject projectTile7 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile7.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(-120, new Vector3(0, 0, 1)) * headingDir;
            projectTile7.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile7.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;

            // Spawn projectile
            GameObject projectTile8 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile8.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(120, new Vector3(0, 0, 1)) * headingDir;
            projectTile8.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.30f;
            projectTile8.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.30f;
        }
        else
        {
            // Spawn projectile
            GameObject projectTilePrefab = Resources.Load("Borrowed/Prefabs/Miscs/BossProjectTile") as GameObject;
            GameObject projectTile = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile.GetComponent<ProjectTileControl>().ProjectTileDirection = headingDir;

            // Spawn projectile
            GameObject projectTile1 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile1.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(-60, new Vector3(0, 0, 1)) * headingDir;

            // Spawn projectile
            GameObject projectTile2 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
            projectTile2.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(60, new Vector3(0, 0, 1)) * headingDir;

            if (_attackPhase == 2)
            {
                // Spawn extra 3 more directions
                // Spawn projectile
                GameObject projectTile3 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
                projectTile3.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(-120, new Vector3(0, 0, 1)) * headingDir;
                projectTile3.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.20f;
                projectTile3.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.20f;

                // Spawn projectile
                GameObject projectTile4 = Instantiate(projectTilePrefab, transform.position, Quaternion.identity);
                projectTile4.GetComponent<ProjectTileControl>().ProjectTileDirection = Quaternion.AngleAxis(120, new Vector3(0, 0, 1)) * headingDir;
                projectTile4.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.20f;
                projectTile4.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.20f;
            }

            if (_attackPhase == 1)
            {
                projectTile.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.10f;
                projectTile.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.10f;
                projectTile1.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.10f;
                projectTile1.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.10f;
                projectTile2.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.10f;
                projectTile2.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.10f;
            }
            else if (_attackPhase == 2)
            {
                projectTile.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.20f;
                projectTile.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.20f;
                projectTile1.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.20f;
                projectTile1.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.20f;
                projectTile2.GetComponent<ProjectTileControl>().ProjectTileDamage *= 1.20f;
                projectTile2.GetComponent<ProjectTileControl>().ProjectTileSpeed *= 1.20f;
            }
        }
    }

    IEnumerator DoFromBeneathAttack()
    {
        // Play some music to warn player first
        _audio.clip = _fromBeneathCastSound;
        _audio.volume = 0.2f;
        _audio.Play();
        _animator.SetBool("Attack", true);
        yield return new WaitForSeconds(2.0f);

        _doAttackAnimation = true;

        // Spawn spike
        SpawnFromBeneathSpike();
        yield return new WaitForSeconds(0.5f);

        // Spawn spike
        SpawnFromBeneathSpike();
        yield return new WaitForSeconds(0.5f);

        // Spawn spike
        SpawnFromBeneathSpike();
        yield return new WaitForSeconds(0.5f);

        // Spawn spike
        SpawnFromBeneathSpike();
        yield return new WaitForSeconds(0.5f);

        // Spawn spike
        SpawnFromBeneathSpike();
        yield return new WaitForSeconds(0.5f);

        if (_attackPhase == 3)
        {
            // Spawn extra 5 spikes in last phase
            SpawnFromBeneathSpike();
            yield return new WaitForSeconds(0.5f);

            // Spawn spike
            SpawnFromBeneathSpike();
            yield return new WaitForSeconds(0.5f);

            // Spawn spike
            SpawnFromBeneathSpike();
            yield return new WaitForSeconds(0.5f);

            // Spawn spike
            SpawnFromBeneathSpike();
            yield return new WaitForSeconds(0.5f);

            // Spawn spike
            SpawnFromBeneathSpike();
            yield return new WaitForSeconds(0.5f);
        }

        _doAttackAnimation = false;
        _delayIdle = true;
        _audio.volume = 0.05f;
        _animator.SetBool("Attack", false);
        StartCoroutine(DelayAttack());
    }

    private void SpawnFromBeneathSpike()
    {
        // Spawn spike
        GameObject spikePrefab = Resources.Load("Borrowed/Prefabs/Miscs/Traps/FromBeneathSpike") as GameObject;
        GameObject spike = Instantiate(spikePrefab, _player.transform.position, Quaternion.identity);
        
        if (_attackPhase == 3)
        {
            spike.GetComponent<FromBeneathSpikeControl>().Damage *= 1.2f;
        }
    }

    public override void Die()
    {
        _battleControl.DefeatBoss();
        _animator.SetBool("Die", true);
        base.Die();
    }

    private void OnDestroy()
    {
        _endLevelTrigger.SetActive(true);
    }

    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(4.0f);
        _delayIdle = false;
    }
}

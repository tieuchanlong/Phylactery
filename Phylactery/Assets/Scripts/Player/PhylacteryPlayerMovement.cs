using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhylacteryPlayerAttack;
using PhylacteryPlayerStatus;


public class PhylacteryPlayerMovement : BasePlayerMovement
{
    // speed constants
    public float speed = 3.0f;
    public float runspeed = 7.0f;

    // stamina variables
    public float totalStam = 100;
    private float currentStam = 100;

    // ammo count
    public int spike_count = 0
    public int stone_count = 0

    protected override void Start()
    {
        currentStam = totalStam;
        _audio = GetComponent<AudioSource>();
        _hpBar = GameObject.FindAnyObjectByType<HPBarControl>();
        _staminaBar = FindAnyObjectByType<StaminaBarControl>();
        _playerRenderer = GetComponent<PlayerCharacterRenderer>();
        base.Start();
    }

    protected override void Update()
    {
        // the angle that the player is looking at follows the mouse position
        Vector3 mouse = Input.mousePosition;

        //if current status is disabled, the player cannot move or attack
        if (!disabled)
        {
            // press Space to attack
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // check  weapon selected, then call function for the attack of the weapon
                if (weaponSelected == 1)
                {
                    Attack1();
                }
                else if (weaponSelected == 2)
                {
                    Attack2();
                }
                else if (weaponSelected == 3)
                {
                    Attack3();
                }
            }

            // press E for special attack
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (weaponselected == 1)
                {
                    SpecialAttack1();
                }
                else if (weaponselected == 2)
                {
                    SpecialAttack2();
                }
                else if (weaponselected == 3)
                {
                    SpecialAttack3();
                }
            }
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical != 0 || horizontal != 0)
        {
            _headingDir = new Vector2(horizontal, vertical);
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                _speed = runspeed;
                _playerRenderer.SetDirection(new Vector2(horizontal, vertical), runDirections);
            }
            else
            {
                _speed = speed;
                _playerRenderer.SetDirection(new Vector2(horizontal, vertical), walkDirections);
            }
        }
        else if (!_doAxeAttackAnimation && !_doDamageAnimation)
        {
            _playerRenderer.SetDirection(_headingDir, staticDirections);
        }

        _staminaBar.UpdateStamina(0.02f * Time.deltaTime);
        base.Update();
    }

    public override void TakeDamage(float damageAmount)
    {
        if (_isDead)
        {
            return;
        }

        if (damageAmount > _currentHP)
        {
            damageAmount = _currentHP;
        }

        base.TakeDamage(damageAmount);

        float hpDamagePercentage = -damageAmount / _maxHP;
        _hpBar.UpdateHP(hpDamagePercentage);
        StartCoroutine(DoDamageAnimation());

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    IEnumerator DoDamageAnimation()
    {
        _doDamageAnimation = true;
        _audio.clip = _damageSound;
        _audio.Play();
        _playerRenderer.SetDirection(_headingDir, hitDirections);
        yield return new WaitForSeconds(1.0f);
        _doDamageAnimation = false;
    }

    public bool IsInAxeAttackRange(Vector3 enemyPos)
    {
        if (!_doAxeAttackAnimation)
        {
            return false;
        }

        if (weaponselected != 1)
        {
            return false;
        }

        float dist = Vector3.Distance(transform.position, enemyPos);

        if (dist > _axeAttackDist)
        {
            return false;
        }

        float angle = Vector3.Dot(_headingDir.normalized, (enemyPos - transform.position).normalized) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) > _axeAttackAngle)
        {
            return false;
        }

        return true;
    }

    public override void AddHP(float hpAmount)
    {
        if (_isDead)
        {
            return;
        }

        if (hpAmount > MaxHP - _currentHP)
        {
            hpAmount = MaxHP - _currentHP;
        }

        base.AddHP(hpAmount);

        float hpAddPercentage = hpAmount / _maxHP;
        _hpBar.UpdateHP(hpAddPercentage);
    }

    public override void Die()
    {
        base.Die();
        StartCoroutine(PlayDeadAnimation());
    }
    
    IEnumerator PlayDeadAnimation()
    {
        // Play death animation
        _playerRenderer.SetDirection(_headingDir, dieDirections);
        yield return new WaitForSeconds(2.0f);

        _gameControl.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ProjectTile")
        {
            ProjectTileControl projectTile = collision.GetComponent<ProjectTileControl>();
            TakeDamage(projectTile.ProjectTileDamage);
            Destroy(collision.gameObject);
        }

        if (collision.tag == "SpikeTrigger")
        {
            SpikeTriggerControl spikeTriggerControl = collision.GetComponent<SpikeTriggerControl>();
            spikeTriggerControl.TriggerSpike();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Spike")
        {
            SpikeControl spike = collision.GetComponent<SpikeControl>();
            spike.GiveDamage(this);
        }
    }


    // ammo pick up
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Thorn")
        {
            thorn_count += 10
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Stone")
        {
            stone_count += 10
            Destroy(collision.gameObject);
        }
    }
}

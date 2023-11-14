using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylacteryPlayerMovement : BasePlayerMovement
{
    // speed constants
    public float speed = 3.0f;
    public float runspeed = 7.0f;

    // stamina variables
    public float totalStam = 100;
    private float specialattStam;
    private float currentStam;

    #region Player Status Variables
    // player status variables
    public bool disabled = false;
    // starts with players only having access to weapon1
    public bool getweapon1 = false;       // weapon1: axe/hammer
    public bool getweapon2 = false;      // weapon2: thorn launcher
    public bool getweapon3 = false;     // weapon3: slingshot
    public bool getweapon4 = false;      // weapon4: undecided :)

    public int weaponselected = 1; // 1-4 corresponding to the weapon1, 2, 3, and 4
                                   // create a variable that updates the angle that the players look at/aim
    #endregion

    #region HP Bar and Stam Bar variables
    private HPBarControl _hpBar;
    #endregion

    #region Animation Directions
    private PlayerCharacterRenderer _playerRenderer;
    private Vector2 _headingDir = new Vector2(0, -1);

    public static string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static string[] walkDirections = { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
    public static string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    public static string[] dieDirections = { "Die N", "Die NW", "Die W", "Die SW", "Die S", "Die SE", "Die E", "Die NE" };
    #endregion

    protected override void Start()
    {
        specialattStam = totalStam / 5;
        currentStam = totalStam;
        _hpBar = GameObject.FindAnyObjectByType<HPBarControl>();
        _playerRenderer = GetComponent<PlayerCharacterRenderer>();
        base.Start();
    }

    protected override void Update()
    {
        // while current stam is lower than total stamina, current stam increases by 0.1 each timeframe
        while (currentStam < totalStam)
        {
            currentStam = currentStam + 0.1f * Time.deltaTime;
        }

        // the angle that the player is looking at follows the mouse position
        Vector3 mouse = Input.mousePosition;


        // change the selected weapon if they are available
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (getweapon1)
            {
                weaponselected = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (getweapon2)
            {
                weaponselected = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (getweapon3)
            {
                weaponselected = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (getweapon4)
            {
                weaponselected = 4;
            }
        }

        //if current status is disabled, the player cannot move or attack
        if (!disabled)
        {
            if (_gameControl.IsLevelCompleted || _isDead)
            {
                return;
            }

            // press Space to attack
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // check  weapon selected, then call function for the attack of the weapon
                if (weaponselected == 1)
                {
                    Attack1();
                }
                else if (weaponselected == 2)
                {
                    Attack2();
                }
                else if (weaponselected == 3)
                {
                    Attack3();
                }
                else
                {
                    Attack4();
                }
            }

            // press E (TBD) for special attack
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
                else
                {
                    SpecialAttack4();
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
        else
        {
            _playerRenderer.SetDirection(_headingDir, staticDirections);
        }

        base.Update();
    }

    // TO BE MOVED TO WEAPON CODING FILE
    void Attack1()
    {
        // for weapon 1
    }

    void Attack2()
    {
        // for weapon 2
    }

    void Attack3()
    {
        // for weapon 3
    }

    void Attack4()
    {
        // for weapon 4
    }

    void SpecialAttack1()
    {
        // for weapon 1
        if (currentStam >= specialattStam)
        {
            currentStam = currentStam - specialattStam;
            //when special attack is used, current stam decreases by special attack stemina, which will vary
        }

    }

    void SpecialAttack2()
    {
        // for weapon 2
        if (currentStam >= specialattStam)
        {
            currentStam = currentStam - specialattStam;
        }

    }

    void SpecialAttack3()
    {
        // for weapon 3
        if (currentStam >= specialattStam)
        {
            currentStam = currentStam - specialattStam;
        }

    }

    void SpecialAttack4()
    {
        // for weapon 4
        if (currentStam >= specialattStam)
        {
            currentStam = currentStam - specialattStam;
        }
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

        if (_currentHP <= 0)
        {
            Die();
        }
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
    }
}

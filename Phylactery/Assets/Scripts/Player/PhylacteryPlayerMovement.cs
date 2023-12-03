using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PhylacteryPlayerAttack;
//using PhylacteryPlayerStatus;


public class PhylacteryPlayerMovement : BasePlayerMovement
{
    // speed constants
    public float speed = 1.0f;
    public float runspeed = 5.0f;

    // stamina variables
    public float totalStam = 100;
    private float currentStam = 100;

    // ammo count
    public int spike_count = 0;
    public int stone_count = 0;

    private AudioSource _audio;
    private HPBarControl _hpBar;
    private StaminaBarControl _staminaBar;
    private PlayerCharacterRenderer _playerRenderer;

    private GameControl _gameControl;
    private int weaponSelected = 1;
    private Vector3 _headingDir = new Vector3(0.0f, 1.0f, 0.0f);

    private bool _doAxeAttackAnimation = false;
    private bool _doThornLaunchAnimation = false;
    private bool _doSlingshotAnimation = false;
    private bool _doSlingshotReleaseAnimation = false;
    private bool _doDamageAnimation = false;

    private const float EPSILON = 0.001f;

    #region Animation Directions
    public static string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static string[] walkDirections = { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
    public static string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    public static string[] axeAttackDirections = { "Axe Attack N", "Axe Attack NW", "Axe Attack W", "Axe Attack SW", "Axe Attack S", "Axe Attack SE", "Axe Attack E", "Axe Attack NE" };
    public static string[] axeChargeDirections = { "Axe Charge N", "Axe Charge NW", "Axe Charge W", "Axe Charge SW", "Axe Charge S", "Axe Charge SE", "Axe Charge E", "Axe Charge NE" };
    public static string[] axeReleaseDirections = { "Axe Release N", "Axe Release NW", "Axe Release W", "Axe Release SW", "Axe Release S", "Axe Release SE", "Axe Release E", "Axe Release NE" };
    public static string[] thornLaunchDirections = { "Thorn Launch N", "Thorn Launch NW", "Thorn Launch W", "Thorn Launch SW", "Thorn Launch S", "Thorn Launch SE", "Thorn Launch E", "Thorn Launch NE" };
    public static string[] slingShotDirections = { "Slingshot N", "Slingshot NW", "Slingshot W", "Slingshot SW", "Slingshot S", "Slingshot SE", "Slingshot E", "Slingshot NE" };
    public static string[] slingShotReleaseDirections = { "Slingshot Release N", "Slingshot Release NW", "Slingshot Release W", "Slingshot Release SW", "Slingshot Release S", "Slingshot Release SE", "Slingshot Release E", "Slingshot Release NE" };
    public static string[] hitDirections = { "Hit N", "Hit NW", "Hit W", "Hit SW", "Hit S", "Hit SE", "Hit E", "Hit NE" };
    public static string[] dieDirections = { "Die N", "Die NW", "Die W", "Die SW", "Die S", "Die SE", "Die E", "Die NE" };
    #endregion

    #region Audio components
    [SerializeField]
    private AudioClip _axeSwingSound;
    [SerializeField]
    private AudioClip _axeSpecialChargeSound;
    [SerializeField]
    private AudioClip _axeSpecialReleaseSound;
    [SerializeField]
    private AudioClip _damageSound;
    [SerializeField]
    private AudioClip _slingshotSpraySound;
    [SerializeField]
    private AudioClip _slingshotSpecialChargeSound;
    [SerializeField]
    private AudioClip _slingshotSpecialReleaseSound;
    #endregion

    #region Axe attack components
    [SerializeField]
    private float _axeAttackDist;

    [SerializeField]
    private float _axeAttackAngle;

    private bool _startCountingAxeChargeTime = false;
    private float _maxAxeChargeTime = 5.0f;
    private float _currentAxeChargeTime = 0.0f;
    private float _maxDashDist = 6.0f;
    private float _curDashDist = 0.0f;
    public bool _startDashing = false;

    [SerializeField]
    private float dashSpeed = 5.0f;
    private float _maxDashTime = 0.0f;
    private float _currentDashTime = 0.0f;
    private TrailRenderer _trailRenderer;
    private BoxCollider2D _colliderBox;

    [SerializeField]
    private BoxCollider2D _dashColliderBox;
    #endregion

    #region Slingshot component
    public GameObject stonePrefab;
    private bool _startCountingSlingshotChargeTime = false;
    private float _currentSlingShotChargeTime = 0.0f;

    [SerializeField]
    private float _maxSlingshotChargeTime = 5.0f;
    #endregion

    #region Footstep Management
    private FootstepSoundControl _footstepSoundControl;
    #endregion

    public int AttackSequence = 0;
    private AmmoHUDControl _ammoHUD;

    public int StoneAmmoCount
    {
        get
        {
            return stone_count;
        }

        set
        {
            stone_count = value;
        }
    }

    public int WeaponSelected
    {
        get
        {
            return weaponSelected;
        }

        set
        {
            weaponSelected = value;
        }
    }

    protected override void Start()
    {
        currentStam = totalStam;
        _audio = GetComponent<AudioSource>();
        _hpBar = GameObject.FindAnyObjectByType<HPBarControl>();
        _staminaBar = FindAnyObjectByType<StaminaBarControl>();
        _playerRenderer = GetComponent<PlayerCharacterRenderer>();
        _gameControl = FindObjectOfType<GameControl>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _colliderBox = GetComponent<BoxCollider2D>();
        _ammoHUD = FindObjectOfType<AmmoHUDControl>();
        _footstepSoundControl = GetComponentInChildren<FootstepSoundControl>();
        base.Start();
    }

    protected override void Update()
    {
        // the angle that the player is looking at follows the mouse position
        Vector3 mouse = Input.mousePosition;

        //if current status is disabled, the player cannot move or attack
        if (!_gameControl.IsLevelCompleted && !_isDead && Time.timeScale > 0)
        {
            if (_startCountingSlingshotChargeTime)
            {
                if (_currentSlingShotChargeTime < _maxSlingshotChargeTime)
                {
                    _currentSlingShotChargeTime += Time.deltaTime;
                }
                currentStam -= 10 * Time.deltaTime;
                _staminaBar.UpdateStamina(-0.1f * Time.deltaTime);
            }

            if (_startCountingAxeChargeTime)
            {
                if (_currentAxeChargeTime < _maxAxeChargeTime)
                {
                    _currentAxeChargeTime += Time.deltaTime;
                }
                _maxDashTime = _currentAxeChargeTime/3.0f;
                currentStam -= 10 * Time.deltaTime;
                _staminaBar.UpdateStamina(-0.1f * Time.deltaTime);
            }

            if (_startDashing)
            {
                // Dash player
                transform.position += _headingDir.normalized * dashSpeed * Time.deltaTime;
                _currentDashTime += Time.deltaTime;
                _curDashDist += dashSpeed * Time.deltaTime;

                if (_currentDashTime >= _maxDashTime || _curDashDist >= _maxDashDist)
                {
                    _doAxeAttackAnimation = false;
                    _startDashing = false;
                    _colliderBox.enabled = true;
                    _dashColliderBox.enabled = false;
                    _trailRenderer.enabled = false;
                }
            }

            if (Input.GetMouseButtonUp(1) || currentStam <= 0)
            {
                if (weaponSelected == 1)
                {
                    if (_doAxeAttackAnimation && _startCountingAxeChargeTime)
                    {
                        AttackRelease1();
                    }
                }
                else if (weaponSelected == 3)
                {
                    if (_doSlingshotAnimation && _startCountingSlingshotChargeTime)
                    {
                        AttackRelease3();
                    }
                }
            }

            // press left mouse to attack
            if (Input.GetMouseButtonDown(0))
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

                AttackSequence++;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && _gameControl.IsWeaponUnlocked(1))
            {
                weaponSelected = 1;
                _ammoHUD.ChangeWeapon(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && _gameControl.IsWeaponUnlocked(2))
            {
                weaponSelected = 2;
                _ammoHUD.ChangeWeapon(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && _gameControl.IsWeaponUnlocked(3))
            {
                weaponSelected = 3;
                _ammoHUD.ChangeWeapon(3);
            }

            // press E for special attack
            if (Input.GetMouseButtonDown(1))
            {
                if (weaponSelected == 1)
                {
                    SpecialAttack1();
                }
                else if (weaponSelected == 2)
                {
                    SpecialAttack2();
                }
                else if (weaponSelected == 3)
                {
                    SpecialAttack3();
                }
            }

            if (!_doAxeAttackAnimation && !_doSlingshotAnimation)
            {
                float vertical = Input.GetAxis("Vertical");
                float horizontal = Input.GetAxis("Horizontal");

                if ((vertical > EPSILON || vertical < -EPSILON) || (horizontal > EPSILON || horizontal < -EPSILON))
                {
                    _headingDir = new Vector2(horizontal, vertical);
                    if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentStam > 0)
                    {
                        _speed = runspeed;
                        _playerRenderer.SetDirection(new Vector2(horizontal, vertical), runDirections);

                        _staminaBar.UpdateStamina(-0.1f * Time.deltaTime);
                        currentStam -= 10.0f * Time.deltaTime;

                        if (currentStam < 0)
                        {
                            currentStam = 0;
                        }

                        _footstepSoundControl.PlayFootstepSound(true);
                    }
                    else
                    {
                        _speed = speed;
                        _playerRenderer.SetDirection(new Vector2(horizontal, vertical), walkDirections);

                        _footstepSoundControl.PlayFootstepSound(false);
                    }
                }
                else if (!_doDamageAnimation)
                {
                    _playerRenderer.SetDirection(_headingDir, staticDirections);
                }
            }

            _staminaBar.UpdateStamina(0.02f * Time.deltaTime);
            currentStam += 2.0f * Time.deltaTime;

            if (currentStam > totalStam)
            {
                currentStam = totalStam;
            }

            // Update ammo HUD
            _ammoHUD.ShowAmmoHUD(weaponSelected != 1);

            if (weaponSelected == 2)
            {
                _ammoHUD.UpdateAmmoCount(spike_count);
            }
            else if (weaponSelected == 3)
            {
                _ammoHUD.UpdateAmmoCount(stone_count);
            }

            base.Update();
        }
    }

    public override void MoveDown()
    {
        if (!_doAxeAttackAnimation && !_doSlingshotAnimation)
        {
            base.MoveDown();
        }
    }

    public override void MoveLeft()
    {
        if (!_doAxeAttackAnimation && !_doSlingshotAnimation)
        {
            base.MoveLeft();
        }
    }

    public override void MoveRight()
    {
        if (!_doAxeAttackAnimation && !_doSlingshotAnimation)
        {
            base.MoveRight();
        }
    }

    public override void MoveUp()
    {
        if (!_doAxeAttackAnimation)
        {
            base.MoveUp();
        }
    }

    public override void TakeDamage(float damageAmount)
    {
        if (_isDead || _doDamageAnimation)
        {
            return;
        }

        if (damageAmount > _currentHP)
        {
            damageAmount = _currentHP;
        }

        ResetAction();
        base.TakeDamage(damageAmount);

        float hpDamagePercentage = -damageAmount / _maxHP;
        _hpBar.UpdateHP(hpDamagePercentage);
        StartCoroutine(DoDamageAnimation());

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    private void ResetAction()
    {
        _doAxeAttackAnimation = false;
        _doSlingshotAnimation = false;
        _startCountingAxeChargeTime = false;
        _startCountingSlingshotChargeTime = false;
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
        if (!_doAxeAttackAnimation || _startCountingAxeChargeTime)
        {
            return false;
        }

        if (weaponSelected != 1)
        {
            return false;
        }

        float dist = Vector3.Distance(transform.position, enemyPos);

        if (dist > _axeAttackDist)
        {
            return false;
        }

        if (!_startDashing)
        {
            float angle = Vector3.Dot(_headingDir.normalized, (enemyPos - transform.position).normalized) * Mathf.Rad2Deg;

            if (Mathf.Abs(angle) > _axeAttackAngle)
            {
                return false;
            }
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

        // Ammo pick up
        if (collision.tag == "ThornAmmo")
        {
            spike_count += 10;
            _ammoHUD.PlayAmmoPickUpSound();
            Destroy(collision.gameObject);
        }

        if (collision.tag == "StoneAmmo")
        {
            stone_count += 9;
            _ammoHUD.PlayAmmoPickUpSound();
            Destroy(collision.gameObject);
        }

        if (collision.tag == "EndLevelTrigger")
        {
            _gameControl.EndLevel();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Spike")
        {
            SpikeControl spike = collision.GetComponent<SpikeControl>();
            spike.GiveDamage(this);
        }

        if (collision.tag == "TutorialTrigger")
        {
            collision.GetComponent<TutorialTriggerControl>().ActivateTutorial();
        }
    }

    void Attack1()
    {
        // axe/ hammer

        if (currentStam > 0 && !_doAxeAttackAnimation)
        {
            currentStam -= 10;
            _staminaBar.UpdateStamina(-0.1f);
            StartCoroutine(DoAxeAttackAnimation());

            // attack range
//             float distance = Vector3.Distance(player.position, transform.position);
// 
//             // action based on the mouse position
//             float direction = Vector3.Dot(mousePos, transform.forward);
// 
//             if (distance < 2.5f)
//             {
//                 if (direction > 0)
//                 {
//                     
//                 }
//             }
// 
//             // player move forward slightly 
//             transform.position += mousePos * Time.deltaTime;
        }
    }

    IEnumerator DoAxeAttackAnimation()
    {
        _doAxeAttackAnimation = true;
        _playerRenderer.SetDirection(_headingDir, axeAttackDirections);
        _audio.clip = _axeSwingSound;
        _audio.Play();

        yield return new WaitForSeconds(1.0f);

        _doAxeAttackAnimation = false;
    }

    void Attack2()
    {
        // thorn launcher
        if (currentStam >= 10)
        {
            

            // attack range
//             float distance = Vector3.Distance(player.position, transform.position);
// 
//             // action based on the mouse position
//             float direction = Vector3.Dot(mousePos, transform.forward);
// 
//             if (distance < 1.0f)
//             {
//                 if (direction > 0)
//                 {
//                     
//                 }
//             }
        }

    }

    IEnumerator DoThornLaunchAnimation()
    {
        _doThornLaunchAnimation = true;
        _playerRenderer.SetDirection(_headingDir, thornLaunchDirections);
        yield return new WaitForSeconds(2.0f);

        _doThornLaunchAnimation = false;
    }

    void Attack3()
    {
        if (!_doSlingshotAnimation)
        {
            if (stone_count >= 3)
            {
                StartCoroutine(DoSlingshotSprayShotAnimation());
            }
            else
            {
                // Play no ammo sound
            }
        }
    }

    IEnumerator DoSlingshotChargeAnimation()
    {
        stone_count -= 1;
        _startCountingSlingshotChargeTime = true;
        _doSlingshotAnimation = true;
        _audio.clip = _slingshotSpecialChargeSound;
        _audio.Play();
        _playerRenderer.SetDirection(_headingDir, slingShotDirections);
        yield return new WaitForSeconds(0.35f);
        _currentSlingShotChargeTime = 0;
    }

    void AttackRelease3()
    {
        // slingshot release
        StartCoroutine(DoSlingshotReleaseAnimation());
    }

    IEnumerator DoSlingshotReleaseAnimation()
    {
        _startCountingSlingshotChargeTime = false;
        _audio.clip = _slingshotSpecialReleaseSound;
        _audio.Play();
        _playerRenderer.SetDirection(_headingDir, slingShotReleaseDirections);
        yield return new WaitForSeconds(0.35f);
        _doSlingshotAnimation = false;

        if (_headingDir.x == 0 && _headingDir.y == 0)
        {
            int a = 0;
            a++;
        }

        // Spawn stone
        GameObject stone = Instantiate(stonePrefab, transform.position, Quaternion.identity);
        ProjectTileControl projectTile = stone.GetComponent<ProjectTileControl>();
        stone.transform.position = transform.position + _headingDir.normalized * 0.3f;
        projectTile.ProjectTileSpeed = _currentSlingShotChargeTime * 5.0f;
        projectTile.ProjectTileLifeTime = _currentSlingShotChargeTime * 5.0f;
        projectTile.ProjectTileDamage = _currentSlingShotChargeTime;
        projectTile.ProjectTileDirection = _headingDir.normalized;
    }

    void SpecialAttack1()
    {
        if (!_doAxeAttackAnimation)
        {
            StartCoroutine(DoAxeChargeAnimation());
        }
    }

    IEnumerator DoAxeChargeAnimation()
    {
        _startCountingAxeChargeTime = true;
        _doAxeAttackAnimation = true;
        _audio.clip = _axeSpecialChargeSound;
        _audio.Play();
        _playerRenderer.SetDirection(_headingDir, axeChargeDirections);
        yield return new WaitForSeconds(0.35f);
        _currentAxeChargeTime = 0;
    }

    void AttackRelease1()
    {
        // axe release
        StartCoroutine(DoAxeReleaseAnimation());
    }

    IEnumerator DoAxeReleaseAnimation()
    {
        _startCountingAxeChargeTime = false;
        _audio.clip = _axeSpecialReleaseSound;
        _audio.Play();
        _playerRenderer.SetDirection(_headingDir, axeReleaseDirections);

        // Dash player
        _startDashing = true;
        _colliderBox.enabled = false;
        _dashColliderBox.enabled = true;
        _currentDashTime = 0.0f;
        _curDashDist = 0.0f;
        _trailRenderer.enabled = true;
        yield return new WaitForSeconds(0.35f);
    }

    void SpecialAttack2()
    {
        // for weapon 2
        if (currentStam >= 10)
        {
            currentStam -= 10;

            // attack range
//             float distance = Vector3.Distance(player.position, transform.position);
//             // action based on the mouse position
//             float direction = Vector3.Dot(mousePos, transform.forward);
// 
//             if (distance < 1.0f)
//             {
//                 if (direction > 0)
//                 {
//                     
//                 }
//             }
        }
    }

    void SpecialAttack3()
    {
        // slingshot
        if (!_doSlingshotAnimation)
        {
            if (stone_count >= 1)
            {
                StartCoroutine(DoSlingshotChargeAnimation());
            }
            else
            {
                // Play no ammo sound
            }
        }
    }

    IEnumerator DoSlingshotSprayShotAnimation()
    {
        stone_count -= 3;
        _doSlingshotAnimation = true;
        _audio.clip = _slingshotSpraySound;
        _audio.Play();
        _playerRenderer.SetDirection(_headingDir, slingShotReleaseDirections);

        // Spawn stone
        Vector3 headingDir = Quaternion.AngleAxis(-80, new Vector3(0, 0, 10)) * _headingDir;
        float angleStep = 160/5;

        for (int i = 0; i < 5; i++)
        {
            bool valid = (headingDir.x < -0.5f || headingDir.x > 0.5f || headingDir.y < -0.5f || headingDir.y > 0.5f);

            if (!valid)
            {
                headingDir = Quaternion.AngleAxis(angleStep, new Vector3(0, 0, 10)) * headingDir.normalized;
                continue;
            }

            GameObject stone = Instantiate(stonePrefab, transform.position, Quaternion.identity);
            ProjectTileControl projectTile = stone.GetComponent<ProjectTileControl>();
            stone.transform.position = transform.position + _headingDir.normalized * 0.3f;
            projectTile.ProjectTileSpeed = 8.0f;
            projectTile.ProjectTileLifeTime = 0.5f;
            projectTile.ProjectTileDamage = 1.0f;
            projectTile.ProjectTileDirection = headingDir;
            headingDir = Quaternion.AngleAxis(angleStep, new Vector3(0, 0, 10)) * headingDir.normalized;
        }

        yield return new WaitForSeconds(1.0f);
        _doSlingshotAnimation = false;
    }
}

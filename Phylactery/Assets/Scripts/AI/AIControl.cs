using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    protected BaseNode _aiRootNode;

    [SerializeField]
    protected float _walkingSpeed;

    [SerializeField]
    protected float _rushSpeed;

    [SerializeField]
    protected float _hp;

    public float HP
    {
        get
        {
            return _hp;
        }
    }

    private bool _isDead = false;

    #region Pathfinding variables
    protected DestinationLocationControl _destination;
    protected AICharacterRenderer _charRenderer;

    [SerializeField]
    protected float _stopDistance;
    #endregion

    #region Damage variables
    private Rigidbody2D _rigidBody;
    protected Vector3 _headingDirection = new Vector3(0, 1, 0);
    private bool _takingDamage = false;
    private float _damageInviciblePeriod = 0.2f;
    private float _currentDamageInviciblePeriod = 0.0f;
    #endregion

    #region Animation Directions
    public static string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static string[] walkDirections = { "Walk N", "Walk NW", "Walk W", "Walk SW", "Walk S", "Walk SE", "Walk E", "Walk NE" };
    public static string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    public static string[] dieDirections = { "Die N", "Die NW", "Die W", "Die SW", "Die S", "Die SE", "Die E", "Die NE" };
    #endregion

    protected PhylacteryPlayerMovement _player;
    protected AudioSource _audio;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _charRenderer = GetComponent<AICharacterRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PhylacteryPlayerMovement>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Update AI Root node actions
        if (_aiRootNode != null)
        {
            GameControl gameControl = FindAnyObjectByType<GameControl>();

            if (!gameControl.IsLevelCompleted && !_takingDamage && _hp > 0)
            {
                _aiRootNode.Update(Time.deltaTime);
            }
        }

        if (_takingDamage)
        {
            _currentDamageInviciblePeriod += Time.deltaTime;
            DoDamageAnimation();

            if (_currentDamageInviciblePeriod >= _damageInviciblePeriod)
            {
                _takingDamage = false;
                _currentDamageInviciblePeriod = 0.0f;
            }
        }

        //DebugTakeDamage();
    }

    public virtual void MoveToDestination(bool running = false, bool usePathfinding = false)
    {
        if (!usePathfinding)
        {
            // Check and rotate AI image
            Vector3 distanceVec = _destination.transform.position - transform.position;

            if (distanceVec.x < 0)
            {
                //transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            distanceVec = new Vector3(distanceVec.x, distanceVec.y, 0.0f); // We don't want the AI to move in depth and mess up each layer depth
            _headingDirection = distanceVec.normalized;

            if (running)
            {
                transform.position += _rushSpeed * Time.deltaTime * distanceVec.normalized;
                _charRenderer.SetDirection(_rushSpeed * Time.deltaTime * distanceVec.normalized, runDirections);
            }
            else
            {
                transform.position += _walkingSpeed * Time.deltaTime * distanceVec.normalized;
                _charRenderer.SetDirection(_walkingSpeed * Time.deltaTime * distanceVec.normalized, walkDirections);
            }
        }
    }

    public virtual void PlayIdleAnimation()
    {
        _charRenderer.SetDirection(_headingDirection, staticDirections);
    }

    public virtual bool ReachedDestination()
    {
        if (_destination == null)
        {
            return true;
        }

        Vector3 distanceVec = _destination.transform.position - transform.position;
        distanceVec = new Vector3(distanceVec.x, distanceVec.y, 0.0f); // We don't want the AI to move in depth and mess up each layer depth

        return (distanceVec.magnitude <= _stopDistance);
    }

    public virtual void TakeDamage(float hpChangeAmount)
    {
        if (_takingDamage || _isDead)
        {
            return;
        }

        _audio.Play();
        _hp -= hpChangeAmount;
        _charRenderer.SetDirection(new Vector2(_headingDirection.x, _headingDirection.y), staticDirections);

        if (_hp > 0)
        {
            _takingDamage = true;
        }
        else if (!_isDead)
        {
            _isDead = true;
            Die();
        }
    }

    public bool IsInPlayerAttackRange()
    {
        return _player.IsInAxeAttackRange(transform.position);
    }

    public bool IsTakingDamage()
    {
        return _takingDamage; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "PlayerAttack")
        {
            TakeDamage(1.0f);
        }
    }

    public virtual void DoDamageAnimation()
    {
        // Play animation
        transform.position -= 5 * Time.deltaTime * _headingDirection.normalized;
    }

    public virtual void Die()
    {
        StartCoroutine(DoDeathAnimation());
    }

    IEnumerator DoDeathAnimation()
    {
        // Play dead animation
        _charRenderer.SetDirection(new Vector2(_headingDirection.x, _headingDirection.y), dieDirections);
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }

    private void DebugTakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !_takingDamage)
        {
            TakeDamage(1.0f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectControl : MonoBehaviour
{
    [SerializeField]
    private int _maxHit;
    private int _currentHitCount = 0;
    private bool _finishInteraction = false;
    private PhylacteryPlayerMovement _player;

    [SerializeField]
    private int _maxDamageFlash = 4;
    [SerializeField]
    private float _timeBtwnDamageFlash = 0.5f;

    private bool _doingEachFlash = false;
    private bool _doingDamageFlash = false;
    private int _currentDamageFlash = 0;
    private Color _originalColor;

    private int _damageSequence = 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _player = FindObjectOfType<PhylacteryPlayerMovement>();
        _originalColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DebugTakeDamage();

        if (_player.IsInAxeAttackRange(transform.position) && _damageSequence <= _player.AttackSequence)
        {
            TakeDamage();
        }

        if (_doingDamageFlash)
        {
            if (!_doingEachFlash)
            {
                if (_currentDamageFlash <= _maxDamageFlash)
                {
                    StartCoroutine(DoDamageFlash());
                }
                else
                {
                    _doingDamageFlash = false;
                }
            }
        }
        else if (_currentHitCount >= _maxHit)
        {
            Interact();
        }
    }

    public void TakeDamage()
    {
        _currentHitCount++;
        _doingDamageFlash = true;
        _damageSequence = _player.AttackSequence + 1;
        _currentDamageFlash = 0;
    }

    protected virtual void Interact()
    {
        _finishInteraction = true;
        Destroy(gameObject);
    }

    protected void DebugTakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage();
        }
    }

    IEnumerator DoDamageFlash()
    {
        _doingEachFlash = true;
        _currentDamageFlash++;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(_timeBtwnDamageFlash);
        GetComponent<SpriteRenderer>().color = _originalColor;
        _doingEachFlash = false;
    }
}

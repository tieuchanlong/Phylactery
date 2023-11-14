using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectControl : MonoBehaviour
{
    [SerializeField]
    private int _maxHit;
    private int _currentHitCount = 0;
    private bool _finishInteraction = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DebugTakeDamage();
    }

    public void TakeDamage()
    {
        _currentHitCount++;

        if (_currentHitCount >= _maxHit)
        {
            Interact();
        }
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
}

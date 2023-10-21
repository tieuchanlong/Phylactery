using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    protected BaseNode _aiRootNode;

    protected float _idleSpeed;
    protected float _rushSpeed;
    protected float _hp;

    public float HP
    {
        get
        {
            return _hp;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Update AI Root node actions
        if (_aiRootNode != null)
        {
            _aiRootNode.Update(Time.deltaTime);
        }
    }
}

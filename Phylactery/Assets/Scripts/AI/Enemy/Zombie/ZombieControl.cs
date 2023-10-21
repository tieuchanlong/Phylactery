using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : EnemyControl
{
    // Start is called before the first frame update
    protected override void Start()
    {
        _aiRootNode = new ZombieNode(this, null);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

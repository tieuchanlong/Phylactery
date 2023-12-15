using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichBossControl : EnemyControl
{
    protected override void Start()
    {
        _aiRootNode = new LichNode(this, null);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoScreamActionNode : BaseNode
{
    public WitchDoScreamActionNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        WitchControl _witchControl = (WitchControl)_aiControl;

        if (!_witchControl.DoScreamAction())
        {
            return NodeStatus.Running;
        }

        return NodeStatus.Success;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoAttackActionNode : BaseNode
{
    public WitchDoAttackActionNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        WitchControl _witchControl = (WitchControl)_aiControl;

        if (!_witchControl.IsInPlayerRange())
        {
            _witchControl.ResetState();
            return NodeStatus.Failure;
        }

        if (!_witchControl.DoAttackAction())
        {
            return NodeStatus.Running;
        }

        return NodeStatus.Success;
    }
}

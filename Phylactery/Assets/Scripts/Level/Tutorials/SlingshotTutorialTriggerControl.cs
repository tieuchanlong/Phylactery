using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotTutorialTriggerControl : TutorialTriggerControl
{
    protected override bool IsUnlocked()
    {
        return _gameControl.IsSlingshotTutorialUnlocked;
    }

    public override void ActivateTutorial()
    {
        _gameControl.UnlockWeapon(3);
        base.ActivateTutorial();
        _gameControl.IsSlingshotTutorialUnlocked = true;
    }
}

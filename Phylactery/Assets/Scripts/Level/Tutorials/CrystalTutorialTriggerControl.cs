using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTutorialTriggerControl : TutorialTriggerControl
{
    protected override bool IsUnlocked()
    {
        return !_gameControl.IsHealingCrystalTutorialUnlocked;
    }

    public override void ActivateTutorial()
    {
        _gameControl.IsHealingCrystalTutorialUnlocked = true;
        base.ActivateTutorial();
    }
}

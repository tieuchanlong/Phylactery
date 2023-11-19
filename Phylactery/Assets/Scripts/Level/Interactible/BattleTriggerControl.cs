using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTriggerControl : MonoBehaviour
{
    private BattleControl _battleControl;

    [SerializeField]
    private List<EnemyControl> _enemies = new List<EnemyControl>();

    // Start is called before the first frame update
    void Start()
    {
        _battleControl = FindObjectOfType<BattleControl>();
    }

    // Update is called once per frame
    void Update()
    {
        bool battleTriggered = false;

        foreach (EnemyControl enemy in _enemies)
        {
            if (enemy && enemy.DetectedPlayer)
            {
                battleTriggered = true;
                break;
            }
        }

        _battleControl.PlayAmbience(battleTriggered, this);
    }
}

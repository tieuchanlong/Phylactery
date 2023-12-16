using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelTrigger : MonoBehaviour
{
    private LichBossControl _boss;
    private BattleControl _battleControl;
    private SpriteRenderer _sprite;

    [SerializeField]
    private List<GameObject> _bossLevelBlocks;

    // Start is called before the first frame update
    void Start()
    {
        _boss = FindObjectOfType<LichBossControl>();
        _boss.gameObject.SetActive(false);
        _battleControl = FindObjectOfType<BattleControl>();
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateBoss()
    {
        _boss.gameObject.SetActive(true);
        _battleControl.PlayBossPhase1();

        foreach (GameObject block in _bossLevelBlocks)
        {
            block.SetActive(true);
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhylacteryPlayerAttack;
using PhylacteryPlayerStatus;


public class PhylacteryPlayerMovement : BasePlayerMovement
{
    // speed constants
    public float speed = 5.0f;
    public float runspeed = 7.0f;

    // stamina variables
    public float totalStam = 100;
    private float currentStam = 100;

    protected override void Start()
    {
        currentStam = totalStam;
        _hpBar = GameObject.FindAnyObjectByType<HPBarControl>();
        base.Start();
    }

    protected override void Update()
    {
        // the angle that the player is looking at follows the mouse position
        Vector3 mouse = Input.mousePosition;

        //if current status is disabled, the player cannot move or attack
        if (!disabled)
        {
            // press Space to attack
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // check  weapon selected, then call function for the attack of the weapon
                if (weaponSelected == 1)
                {
                    Attack1();
                }
                else if (weaponSelected == 2)
                {
                    Attack2();
                }
                else if (weaponSelected == 3)
                {
                    Attack3();
                }
            }

            // press E for special attack
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (weaponselected == 1)
                {
                    SpecialAttack1();
                }
                else if (weaponselected == 2)
                {
                    SpecialAttack2();
                }
                else if (weaponselected == 3)
                {
                    SpecialAttack3();
                }
            }
        }

        base.Update();
    }

    public override void TakeDamage(float damageAmount)
    {
        if (damageAmount > _currentHP)
        {
            damageAmount = _currentHP;
        }

        base.TakeDamage(damageAmount);

        float hpDamagePercentage = -damageAmount / _maxHP;
        _hpBar.UpdateHP(hpDamagePercentage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ProjectTile")
        {
            ProjectTileControl projectTile = collision.GetComponent<ProjectTileControl>();
            TakeDamage(projectTile.ProjectTileDamage);
            Destroy(collision.gameObject);
        }
    }
}

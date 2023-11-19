using UnityEngine;

using System.Collections;

public class PlayerAttack : MonoBehaviour {
    public GameObject target;
    public float attackTimer;
    public float coolDown;

    // starts with players only having access to weapon1
    public bool getWeapon1 = true;       // weapon1: axe/hammer
    public bool getWeapon2 = false;      // weapon2: thorn launcher
    public bool getWeapon3 = false;     // weapon3: slingshot

    public int weaponSelected = 1; // 1-4 corresponding to the weapon

    void Start () {
        attackTimer = 0;
        coolDown = 2.0f;

        //body = GetComponent<Rigidbody2D>(); // access components  
    }
}
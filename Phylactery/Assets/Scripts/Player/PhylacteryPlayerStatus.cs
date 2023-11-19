using UnityEngine;

using System.Collections;

public class PlayerStatus : MonoBehaviour {

    // health variables
    public int maxHealth = 100;
    public int curHealth = 100;

    // stamina variables
    public float maxStamina = 100;
    public float curStamina = 100;
    
    #region Player Status Variables
    // starts with players only having access to weapon1
    public bool getweapon1 = false;       // weapon1: axe/hammer
    public bool getweapon2 = false;      // weapon2: thorn launcher
    public bool getweapon3 = false;     // weapon3: slingshot

    public int weaponselected = 1; // corresponding to the weapon type
    #endregion
    
    
    #region HP Bar and Stam Bar variables
    private HPBarControl _hpBar;
    #endregion

    void Start () {
        
    }

    // Update is called once per frame
    void Update () {

    }
}
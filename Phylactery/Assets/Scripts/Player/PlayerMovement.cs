using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // speed constants
    public float speed = 5.0f;
    public float runspeed = 7.0f;

    // stamina variables
    public float totalStam = 100;
    public float specialattStam = totalStam/5;
    public float currentStam = totalStam;
    
    // player status variables
    public bool disabled = FALSE;
    // starts with players only having access to weapon1
    public bool getweapon1 = TRUE;       // weapon1: axe/hammer
    public bool getweapon2 = FALSE;      // weapon2: thorn launcher
    public bool getweapon3 = FALSE;     // weapon3: slingshot
    public bool getweapon4 = FALSE;      // weapon4: undecided :)
    
    public int weaponselected = 1; // 1-4 corresponding to the weapon1, 2, 3, and 4
    // create a variable that updates the angle that the players look at/aim

    private Rigidbody2D body;
    private SceneHandler scene;
    

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); // access components  
    }

    // Update is called once per frame
    void Update()
    {
        // while current stam is lower than total stamina, current stam increases by 0.1 each timeframe
        while (currentStam < totalStam){
            currentStam = currentStam + 0.1* Time.deltaTime;
        }

        // the angle that the player is looking at follows the mouse position
        Vector3 mouse = Input.mousePosition;


        // change the selected weapon if they are available
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
                if (getweapon1 == TRUE)
                {
                    weaponselected = 1;
                }
            }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
                if (getweapon2 == TRUE)
                {
                    weaponselected = 2;
                }
            }

        if(Input.GetKeyDown(KeyCode.Alpha3)) {
                if (getweapon3 == TRUE)
                {
                    weaponselected = 3;
                }
            }

        if(Input.GetKeyDown(KeyCode.Alpha4)) {
                if (getweapon4 == TRUE)
                {
                    weaponselected = 4;
                }
            }

        //if current status is disabled, the player cannot move or attack
        if (disabled == FALSE){

            // press Space to attack
            if(Input.GetKeyDown(KeyCode.Space)) {
                // check  weapon selected, then call function for the attack of the weapon
                if (weaponselected == 1)
                {
                    Attack1();
                }
                else if(weaponselected == 2) 
                {
                    Attack2();
                } else if (weaponselected == 3) 
                {
                    Attack3();
                } else {
                    Attack4();
                }
            }

            // press E (TBD) for special attack
            if(Input.GetKeyDown(KeyCode.E)) {
                if (weaponselected == 1)
                {
                    SpecialAttack1();
                }
                else if(weaponselected == 2) 
                {
                    SpecialAttack2();
                } else if (weaponselected == 3) 
                {
                    SpecialAttack3();
                } else {
                    SpecialAttack4();
                }
            }

            // press D to move right
            if(Input.GetKeyDown(KeyCode.D)) {
                MoveRight();
            }
        
            // press W to move up
            if(Input.GetKeyDown(KeyCode.W)) {
                MoveUp();
            }

            // press A to move left
            if(Input.GetKeyDown(KeyCode.A)) {
                MoveLeft();

            // press S to move down
            if(Input.GetKeyDown(KeyCode.S)) {
                MoveDown();
            }        
        }
    }

    // Move to the right every frame *per second*
    void MoveRight() {   
        //if left shift key is pressed at the same time, use runspeed instead of speed 
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            transform.position +=
                new Vector3(runspeed, 0.0f, 0.0f) * Time.deltaTime;
        } else {
            transform.position +=
                new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    void MoveLeft() { 
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            transform.position +=
                new Vector3(-runspeed, 0.0f, 0.0f) * Time.deltaTime;
        } else {
            transform.position +=
                new Vector3(-speed, 0.0f, 0.0f) * Time.deltaTime;
        }  
    }

    void MoveUp() {   
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            transform.position +=
                new Vector3(0.0f, runspeed, 0.0f) * Time.deltaTime;
        } else {
            transform.position +=
                new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;
        }  
    }

    void MoveDown() {   
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            transform.position +=
                new Vector3(0.0f, -runspeed, 0.0f) * Time.deltaTime;
        } else {
            transform.position +=
                new Vector3(0.0f, -speed, 0.0f) * Time.deltaTime;
        }  

    }


    // TO BE MOVED TO WEAPON CODING FILE
    void Attack1(){
        // for weapon 1
    }

    void Attack2(){
        // for weapon 2
    }

    void Attack3(){
        // for weapon 3
    }

    void Attack4(){
        // for weapon 4
    }

    void SpecialAttack1(){
        // for weapon 1
        if (currentStam >= specialattStam){
            currentStam = currentStam - SpecialattStam;
            //when special attack is used, current stam decreases by special attack stemina, which will vary
        }
       
    }

    void SpecialAttack2(){
        // for weapon 2
        if (currentStam >= specialattStam){
            currentStam = currentStam - SpecialattStam; 
        }
       
    }

    void SpecialAttack3(){
        // for weapon 3
        if (currentStam >= specialattStam){
            currentStam = currentStam - SpecialattStam;
        }
       
    }

    void SpecialAttack4(){
        // for weapon 4
        if (currentStam >= specialattStam){
            currentStam = currentStam - SpecialattStam;
        }
    }

}
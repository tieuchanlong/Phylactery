using UnityEngine;

using System.Collections;

public class PlayerAttack : MonoBehaviour {
    public GameObject target;
    public float attackTimer;
    public float coolDown;

    // starts with players only having access to weapon1
    public bool getWeapon1 = TRUE;       // weapon1: axe/hammer
    public bool getWeapon2 = FALSE;      // weapon2: thorn launcher
    public bool getWeapon3 = FALSE;     // weapon3: slingshot

    public int weaponSelected = 1; // 1-4 corresponding to the weapon

    void Start () {
        attackTimer = 0;
        coolDown = 2.0f;

        body = GetComponent<Rigidbody2D>(); // access components  
    }


    void Update () {
        if(attackTimer > 0)
            attackTimer -= Time.deltaTime;

        if(attackTimer < 0)
            attackTimer = 0;

        // change the selected weapon if they are available
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            if (getWeapon1 == TRUE)
                    weaponSelected = 1;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            if (getWeapon2 == TRUE)
                    weaponSelected = 2;
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            if (getWeapon3 == TRUE)
                    weaponSelected = 3;
        }    
    }

    void Attack1(){
        // axe/ hammer

        if (currentStam >= 10){
            currentStam -= 10; 

            // attack range
            float distance = Vector3.Distance(player.position, transform.position);

            // action based on the mouse position
            float direction = Vector3.Dot(mousePos, transform.forward);

            if(distance < 2.5f) {
                if(direction > 0) {
                    EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
                    eh.AddCurrentHealth(-20);
                }
            }

            // player move forward slightly 
            transform.position += mousePos * Time.deltaTime;     
        } 
    }

    void Attack2(){
        // thorn launcher
        if (currentStam >= 10){
            currentStam -= 10; 

            // attack range
            float distance = Vector3.Distance(player.position, transform.position);

            // action based on the mouse position
            float direction = Vector3.Dot(mousePos, transform.forward);

            if(distance < 1.0f) {
                if(direction > 0) {
                    EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
                    eh.AddCurrentHealth(-10);
                }
            }
        } 
 
    }  

    void Attack3(float chargeTime){
        // slingshot
        if (currentStam >= 10){
            currentStam -= 10; 

            // attack range
            float distance = Vector3.Distance(player.position, transform.position);

            // action based on the mouse position
            float direction = Vector3.Dot(mousePos, transform.forward);

            if(distance < 1.0f) {
                if(direction > 0) {
                    EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
                    eh.AddCurrentHealth(-chargeTime * 1.5);
                }
            }  
        } 
    }


    void SpecialAttack1(){
        // for weapon 1
        if (currentStam >= 20){
            currentStam -= 20; 

            // attack range
            float distance = Vector3.Distance(player.position, transform.position);

            // action based on the mouse position
            float direction = Vector3.Dot(mousePos, transform.forward);

            if(distance < 1.0f) {
                if(direction > 0) {
                    EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
                    eh.AddCurrentHealth(-30);
                }
            }  
        } 
       
    }

    void SpecialAttack2(){
        // for weapon 2
        if (currentStam >= 10){
            currentStam -= 10; 

            // attack range
            float distance = Vector3.Distance(player.position, transform.position);
            // action based on the mouse position
            float direction = Vector3.Dot(mousePos, transform.forward);

            if(distance < 1.0f) {
                if(direction > 0) {
                    EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
                    eh.AddCurrentHealth(-30);
                }
            }
        }        
    }

    void SpecialAttack3(){
        // for weapon 3
        if (currentStam >= 10){
            currentStam -= 10; 
            
            // attack range
            float distance = Vector3.Distance(player.position, transform.position);

            // action based on the mouse position
            float direction = Vector3.Dot(mousePos, transform.forward);

            if(distance < 1.0f) {
                if(direction > 0) {
                    EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
                    eh.AddCurrentHealth(-10);
                }
            }
        }        
    }
}
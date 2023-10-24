using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody2D body;
    //private SceneHandler scene;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        // access component
    }

    // Update is called once per frame
    void Update()
    {
        // press D to move right
        if(Input.GetKeyDown(KeyCode.D)) {
            MoveRight();
        }
      
        // press W to move up
        if(Input.GetKeyDown(KeyCode.W)) {
            MoveUp();
        }

        // press A to move left
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }

        // press S to move left
        if(Input.GetKeyDown(KeyCode.S)) {
            MoveDown();
        }        
    }

    // Move to the right every frame *per second*
    void MoveRight() {   
            transform.position +=
                new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
    }

    void MoveLeft() {   
            transform.position +=
                new Vector3(-speed, 0.0f, 0.0f) * Time.deltaTime;
    }

    void MoveUp() {   
            transform.position +=
                new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;
    }

    void MoveDown() {   
            transform.position +=
                new Vector3(0.0f, -speed, 0.0f) * Time.deltaTime;
    }
}
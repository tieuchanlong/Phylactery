using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 300.0f;

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
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveRight();
        }

        // press A to move left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        // press space to jump
        if(Input.GetKeyDown(KeyCode.Space)) {
            Jump();
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

    void Jump(){
            body.AddForce(Vector2.up * jumpForce);
    }
}
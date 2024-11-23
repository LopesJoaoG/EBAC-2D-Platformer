using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Vector2 friction = new Vector2(-.1f, 0);

    public float speed;
    public float forceJump;

    // Update is called once per frame
    void Update()
    {
        Jump();
        SideMovement();
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * forceJump;
        }
    }

    private void SideMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(-speed, myRigidbody.position.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(speed, myRigidbody.position.y);
        }

        if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= friction;
        }
        else if (myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += friction;
        }
    }
}

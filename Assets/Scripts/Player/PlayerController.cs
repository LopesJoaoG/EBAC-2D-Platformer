using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Vector2 friction = new Vector2(-.1f, 0);

    [Header("Speed")]
    public float speed;
    public float speedRun;
    private float currentSpeed;

    [Header("Jump")]
    public float forceJump;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = 0.4f;
    public Ease ease = Ease.OutBack;

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
            myRigidbody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidbody.transform);

            ScaleJump();
        }
    }

    private void SideMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = speedRun;
        }
        else
        {
            currentSpeed = speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(-currentSpeed, myRigidbody.position.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(currentSpeed, myRigidbody.position.y);
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

    private void ScaleJump()
    {
        myRigidbody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}

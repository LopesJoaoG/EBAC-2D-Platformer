using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    private float currentSpeed;
    public Rigidbody2D myRigidbody;

    [Header("Setup")]
    public SOPlayer playerSetup;

    public Animator animator;
    public HealthBase healthBase;
    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }
    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(playerSetup.triggerDeath); ;
    }
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
            myRigidbody.velocity = Vector2.up * playerSetup.forceJump;
            //myRigidbody.transform.localScale = Vector2.one;

            //DOTween.Kill(myRigidbody.transform);

            //ScaleJump();
        }
    }

    private void SideMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = playerSetup.speedRun;
            animator.speed = 2;
        }
        else
        {
            currentSpeed = playerSetup.speed;
            animator.speed = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(-currentSpeed, myRigidbody.position.y);

            if (myRigidbody.transform.localScale.x != -1)
            {
                myRigidbody.transform.DOScaleX(-1, playerSetup.swipeAnimation);
            }

            animator.SetBool(playerSetup.boolRun, true);;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(currentSpeed, myRigidbody.position.y);

            if (myRigidbody.transform.localScale.x != 1)
            {
                myRigidbody.transform.DOScaleX(1, playerSetup.swipeAnimation);
            }

            animator.SetBool(playerSetup.boolRun, true);
        }
        else
        {
            animator.SetBool(playerSetup.boolRun, false);
        }

        if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= playerSetup.friction;
        }
        else if (myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += playerSetup.friction;
        }
    }

    private void ScaleJump()
    {
        myRigidbody.transform.DOScaleY(playerSetup.jumpScaleY, playerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(playerSetup.ease);
        myRigidbody.transform.DOScaleX(playerSetup.jumpScaleX, playerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(playerSetup.ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}

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
    private bool isJumping = false;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = 0.4f;
    public Ease ease = Ease.OutBack;

    [Header("Animation")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public Animator animator;
    public float swipeAnimation = .1f;

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
        animator.SetTrigger(triggerDeath);
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
            myRigidbody.velocity = Vector2.up * forceJump;
            //myRigidbody.transform.localScale = Vector2.one;

            //DOTween.Kill(myRigidbody.transform);

            //ScaleJump();
        }
    }

    private void SideMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = speedRun;
            animator.speed = 2;
        }
        else
        {
            currentSpeed = speed;
            animator.speed = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(-currentSpeed, myRigidbody.position.y);

            if (myRigidbody.transform.localScale.x != -1)
            {
                myRigidbody.transform.DOScaleX(-1, swipeAnimation);
            }

            animator.SetBool(boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(currentSpeed, myRigidbody.position.y);

            if (myRigidbody.transform.localScale.x != 1)
            {
                myRigidbody.transform.DOScaleX(1, swipeAnimation);
            }

            animator.SetBool(boolRun, true);
        }
        else
        {
            animator.SetBool(boolRun, false);
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

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}

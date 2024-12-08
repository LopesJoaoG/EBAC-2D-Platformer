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

    public Collider2D collider2d;
    public float distToGround;
    public float spaceToGround;

    public ParticleSystem walkVFX;
    public ParticleSystem jumpVFX;

    public AudioSource jumpAudioSource;
    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        if(collider2d != null)
        {
            distToGround = collider2d.bounds.extents.y;
        }
    }

    public bool isGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }
    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(playerSetup.triggerDeath); ;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded();
        Jump();
        SideMovement();
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            myRigidbody.velocity = Vector2.up * playerSetup.forceJump;
            //myRigidbody.transform.localScale = Vector2.one;

            //DOTween.Kill(myRigidbody.transform);

            //ScaleJump();
            PlayJumpVFX();
            PlayJumpAudio();
        }
    }

    private void PlayJumpVFX()
    {
        if (jumpVFX != null) jumpVFX.Play();

    }

    private void PlayJumpAudio()
    {
        if (jumpAudioSource != null) jumpAudioSource.Play();

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
            myRigidbody.velocity = new Vector2(-currentSpeed, myRigidbody.velocity.y);

            if (myRigidbody.transform.localScale.x != -1)
            {
                myRigidbody.transform.DOScaleX(-1, playerSetup.swipeAnimation);
            }

            animator.SetBool(playerSetup.boolRun, true);;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            myRigidbody.velocity = new Vector2(currentSpeed, myRigidbody.velocity.y);

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

        if (isGrounded())
        {
            if (walkVFX != null) walkVFX.Play();
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

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayer : ScriptableObject
{
    public Vector2 friction = new Vector2(-.1f, 0);

    [Header("Speed")]
    public float speed;
    public float speedRun;

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
    public float swipeAnimation = .1f;
}

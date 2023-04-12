using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    public Animation player; 
    
    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public Vector2 velocity;
    public float speed;
    public float speedRun;
    public float forceJump;
    public Ease ease = Ease.OutBack;


    [Header("Animation Setup")]
    public float JumpScaleY = 1.5f;
    public float JumpScaleX = .7f;
    public float AnimationDuration = .3f;
    

    [Header("Animation player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public float playerSwipeDuration = .1f;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{   
    public Rigidbody2D myRigidbody;
    public HealthBase healthBase;

    private bool isJumping;
    
    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public Vector2 velocity;
    public float speed;
    public float speedRun;
    public float forceJump;


    [Header("Animation Setup")]
    //...
    //codigo antigo antes de usar scriptable objects 
    //public float JumpScaleY = 1.5f;
    //public float JumpScaleX = .7f;
    //public float AnimationDuration = .3f;
    //...
    public SOFloat soJumpScaleY;
    public SOFloat soJumpScaleX;
    public SOFloat soAnimationDuration;


    public Ease ease = Ease.OutBack;

    [Header("Animation player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public Animator animator;
    public float playerSwipeDuration = .1f;

    private float _currentSpeed;


    private void Awake() 
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
            healthBase.OnKill -= OnPlayerKill;

            animator.SetTrigger(triggerDeath);
    }

    private void Update() 
    {
        HandleMoviment();
        HandleJump();
    }

    private void HandleMoviment()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        _currentSpeed = speedRun;
        else
        _currentSpeed = speed;

        if(Input.GetKey(KeyCode.LeftArrow))    
        {
            //myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            //codigo de referencia.
            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
            if(myRigidbody.transform.localScale.x != -1)
            {
                myRigidbody.transform.DOScaleX(-1, playerSwipeDuration);
            }
            animator.SetBool(boolRun, true);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            //codigo de referencia.
            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
            if(myRigidbody.transform.localScale.x != 1)
            {
                myRigidbody.transform.DOScaleX(1, playerSwipeDuration);
            }
            animator.SetBool(boolRun, true);
        }
        else
        {
            animator.SetBool(boolRun, false);
        }
        

        if(myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += friction;
        }
        else if(myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            myRigidbody.velocity = Vector2.up * forceJump;
            myRigidbody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidbody.transform);

            HandleScaleJump();
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void HandleScaleJump()
    {
        myRigidbody.transform.DOScaleY(soJumpScaleY.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleX(soJumpScaleX.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(ease);

    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}

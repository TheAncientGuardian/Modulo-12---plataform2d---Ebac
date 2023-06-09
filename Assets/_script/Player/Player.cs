using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{   
    public Rigidbody2D myRigidbody;
    public HealthBase healthBase;

    
    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    //public Animator animator;

    private float _currentSpeed;
    private bool isJumping;
    
    private Animator _currentPlayer;


    private void Awake() 
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);
    }

    private void OnPlayerKill()
    {
            healthBase.OnKill -= OnPlayerKill;

            _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update() 
    {
        HandleMoviment();
        HandleJump();
    }

    private void HandleMoviment()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        _currentSpeed = soPlayerSetup.speedRun;
        else
        _currentSpeed = soPlayerSetup.speed;

        if(Input.GetKey(KeyCode.LeftArrow))    
        {
            //myRigidbody.MovePosition(myRigidbody.position - velocity * Time.deltaTime);
            //codigo de referencia.
            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
            if(myRigidbody.transform.localScale.x != -1)
            {
                myRigidbody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
            //codigo de referencia.
            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
            if(myRigidbody.transform.localScale.x != 1)
            {
                myRigidbody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }
        

        if(myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += soPlayerSetup.friction;
        }
        else if(myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            myRigidbody.velocity = Vector2.up * soPlayerSetup.forceJump;
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
        myRigidbody.transform.DOScaleY(soPlayerSetup.JumpScaleY, soPlayerSetup.AnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidbody.transform.DOScaleX(soPlayerSetup.JumpScaleX, soPlayerSetup.AnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);

    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    public int damage = 10;

    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerKill = "Kill";


    public HealthBase healthBase;

    public float timeToDestroy =1f;

    private void Awake() 
    {
        if(healthBase != null)
        {
            healthBase.OnKill +=OnEnemyKill;
        }
    }

    private void OnEnemyKill()
    {
            healthBase.OnKill -=OnEnemyKill;
            PLayKillAnimation();
            Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log(collision.transform.name);

        var health = collision.gameObject.GetComponent<HealthBase>();

        if(health != null)
        {
            health.Damage(damage);   
            PLayAttackAnimation();             
        }
    }

    private void PLayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PLayKillAnimation()
    {
        animator.SetTrigger(triggerKill);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }
}

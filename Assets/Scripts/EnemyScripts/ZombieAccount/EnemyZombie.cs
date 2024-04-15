using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : Enemy
{
    public float ZombieSpeed;
    public float StartWaitTime;
    private float WaitTime;

    public Transform TargetPoint;   //the next position the zombie account will move to. 
    public Transform RightUpPoint; //represent upper right point.
    public Transform LeftLowPoint; //represent lower left point.

    private Animator ZombieAnim;  //get the animator of the troll. 

    private bool ZombieDead = false;  //deter whether the troll should dead. 

    public new void Start()
    {
        base.Start();

        LifeValue = 20;
        AttackDamage = 10;   //set the attack damage value of zombie account to be 10; 
        WaitTime = StartWaitTime;
        TargetPoint.position = GenerateRamdomPosition();

        ZombieAnim = GetComponent<Animator>();  //get the animator of troll.
    }

    public new void Update()
    {
        base.Update();
        if (ZombieDead != true)
        {
            EnemyCruise();   //the function that enable the zombie account to cruise; 
        }

        EnemyDead();
    }

    private void EnemyCruise()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetPoint.position, ZombieSpeed * Time.deltaTime);  //Movetowards is used to make the object move from starting point to a Target point. 

        if (Vector2.Distance(transform.position, TargetPoint.position) < 0.1f)   //When the enemy moves close enough to the target point. 
        {
            if (WaitTime <= 0)
            {
                TargetPoint.position = GenerateRamdomPosition();    //create a new target position. 
                WaitTime = StartWaitTime;
            }

            else
            {
                WaitTime -= Time.deltaTime;
            }
        }
    }
    Vector2 GenerateRamdomPosition()
    {
        Vector2 NextTarget = new Vector2(Random.Range(LeftLowPoint.position.x, RightUpPoint.position.x),Random.Range(LeftLowPoint.position.y, RightUpPoint.position.y));

        return NextTarget;
    }

    public void EnemyDead()   //remove the enemy object when enemy is killed. 
    {
        if (LifeValue <= 0)
        {
            ZombieAnim.SetBool("IsZombiedead", true);
            ZombieDead = true;

            StartCoroutine(TrollRemove());  //the zombie should not be directly removed after being killed, there should be a short delay time.

        }
    }


    IEnumerator TrollRemove()
    {
        yield return new WaitForSeconds(1);   // the zombie account will disappear 1 seconds later after being killed. 

        Destroy(gameObject);
    }


}
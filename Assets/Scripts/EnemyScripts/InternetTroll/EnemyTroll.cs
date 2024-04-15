using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTroll : Enemy
{
    // Start is called before the first frame update

    public float ChaseDistance;    //This valuable determine the boundary that the enemy will chase the player.  
    public float ChaseSpeed;   //the chase speed of enemy. 

    private Transform PlayerPosition;   //get the player's position. 

    private Animator TrollAnim;  //get the animator of the troll. 

    private bool TrollDead = false;  //deter whether the troll should dead. 
    public new void Start()
    {
        ChaseSpeed = 3;
        ChaseDistance = 40;
        LifeValue = 20; 

        base.Start();
        AttackDamage = 10;
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        TrollAnim = GetComponent<Animator>();  //get the animator of troll.

    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        if(PlayerPosition != null && TrollDead != true)
        {
            EnemyChasing();   //make enemy chasing the player. 
        }

        EnemyDead();   //make the enemy troll dead when life value = 0;
    }

    private void EnemyChasing()      //Make the enmey can chase the player.  
    {
        float Distance;

        Distance = (transform.position - PlayerPosition.position).sqrMagnitude;   //calculate the distance between player and enemy. 

        if((Distance < ChaseDistance))     //check whether the enemy should start the chasing. 
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerPosition.position, ChaseSpeed * Time.deltaTime);
        }
    }

    public void EnemyDead()   //remove the enemy object when enemy is killed. 
    {
        if (LifeValue <= 0)
        {
            TrollAnim.SetBool("IsTrolldead", true);
            TrollDead = true;

            StartCoroutine(TrollRemove());  //the troll should not be directly removed after being killed, there should be a short delay time.

        }
    }


    IEnumerator TrollRemove()
    {
        yield return new WaitForSeconds(1);   // the troll will disappear 1 seconds later after being killed. 

        Destroy(gameObject);
    }
}

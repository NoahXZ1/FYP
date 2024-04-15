using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBoss : Enemy
{
    // Start is called before the first frame update

    public float ChaseDistance;    //This valuable determine the boundary that the enemy will chase the player.  
    public float ChaseSpeed;   //the chase speed of enemy. 

    private Transform PlayerPosition;   //get the player's position. 

    private Animator BossAnim;

    private bool BossDead = false; 

    public new void Start()
    {
        LifeValue = 300;   //Set the life value of boss. 
        ChaseDistance = 120;
        ChaseSpeed = 2;

        AttackDamage = 13;

        base.Start();

        BossLifeBar.Maxlife = LifeValue;    //set the initial value of boss life. 
        BossLifeBar.CurrentLife = LifeValue;   

        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        BossAnim = GetComponent<Animator>();  //get the animator of boss.

    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        ChangeLifeBar();   //update the life bar value. 

        if (PlayerPosition != null && BossDead != true)  //only when the boss is alive can it chasing the player. 
        {
            EnemyChasing();        //make enemy chasing the player. 
        }

        EnemyDead();
    }

    private void EnemyChasing()     //Make the enmey can chase the player.  
    {
        float Distance;

        Distance = (transform.position - PlayerPosition.position).sqrMagnitude;   //calculate the distance between player and enemy. 

        FacingDirection();  //change the facing direction of enemy to player. 

        if (Distance < 12)  //when player close to boss, the boss will play the attacking animation. 
        {
            BossAnim.SetTrigger("BossAttacking");
        }

        if ((Distance < ChaseDistance))               //check whether the enemy should start the chasing. 
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerPosition.position, ChaseSpeed * Time.deltaTime);
        }
    }

    public void EnemyDead()   //remove the enemy object when enemy is killed. 
    {
        if (LifeValue <= 0)
        {
            BossAnim.SetBool("Isdead",true);
            BossDead = true;

            StartCoroutine(BossRemove());  //the boss should not be directly removed after being killed, there should be a short delay time.
            
        }
    }


    IEnumerator BossRemove()
    {
        yield return new WaitForSeconds(2);   // the boss will disappear 2 seconds later after being killed. 

        Destroy(gameObject);
    }

    private void FacingDirection() 
    { 
        float relativePosition = PlayerPosition.position.x - transform.position.x;


        if (relativePosition > 0)
        {

            transform.localRotation = Quaternion.Euler(0, 0, 0);   //when enemy is in the left of player, enemy facing right. 
        }
        else if (relativePosition < 0)                     //when enemy is in the left of player, enemy facing right.
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void ChangeLifeBar()  // dynamically update the value of life bar of the boss
    {
        BossLifeBar.CurrentLife = LifeValue;
    }

}

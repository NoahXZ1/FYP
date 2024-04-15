using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // This abstract is for all kinds of enemys, the valuable and functions that used by all enemys are defined here. 

    public int LifeValue = 20;

    private SpriteRenderer EnemySR;  //SpriteRenderer is used to change the color of game object. 
    private Color EnemyColor;  //the original color of the enemy. 

    private float FlashTime = 0.2f;  //represent the flash time of enemy being hurt;

    public int AttackDamage;   //the damage value of enemy cause to player. 
   

    private PlayerLife Playerlife;
    public void Start()
    {
        Playerlife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();   //get the valuable in PlayerLife by using Playerlife. 
        GetOriginal();
    }

    // Update is called once per frame
    public void Update()
    {
       
    }

    public void GetDamage(int damage)   //make the enemy can be damaged by the attack of player. 
    {
        LifeValue -= damage;

        EnemySR.color = Color.red;        //change the color of enemy to red. 
        Invoke("ColorBack", FlashTime); //Invoke() is used to delay the implementation of ColorBack for a short time, creating the flash effect. 
    }


    public void ColorBack()  //change the color of enemy from red to its original color. 
    {
        EnemySR.color = EnemyColor;
    }

    public void GetOriginal()   //Save the original color of enemy. 
    {

        EnemySR = GetComponent<SpriteRenderer>();
        EnemyColor = EnemySR.color;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
                if (Playerlife != null)    //Ensure that the Playerlife is got successfully, avoid bugs.  
                {
                    Playerlife.PlayerHurt(AttackDamage);
                }
            
                
        }
    }

    
}

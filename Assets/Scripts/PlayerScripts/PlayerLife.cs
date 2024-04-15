using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update

    public int Playerlife;
    public int FlashTime;  //represent the time player flash after being attack. 
    public float Time;  //represent hong long player will flash. 

    private bool CanBeDamage = true;  //used to control the frequency for player to get hurt in OnTriggerStay2D() by enemy when triggered the collider of enemy. 
    public float DamageInterval = 0.6f;  //the interval between two times of damage taken by player when continuously stay in enemy's collider. 

    private Renderer PlayerRender;
    private Rigidbody2D PlayerRb;

    private Animator PlayerAnim;

    void Start()
    {
        PlayerRender = GetComponent<Renderer>();
        PlayerLifeBar.Maxlife = Playerlife;   //get the player life value to the text. 
        PlayerLifeBar.CurrentLife = Playerlife;

        PlayerAnim = GetComponent<Animator>();

        PlayerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHurt(int damage)  //implemented when player is hurt by the enemy. 
    {
        if (CanBeDamage)
        {
            CanBeDamage = false;    //set the player can not be hurt. 

            Playerlife -= damage;
            PlayerLifeBar.CurrentLife = Playerlife;   // update the text value showed in the lifebar. 

            if (Playerlife <= 0)
            {
                Playerlife = 0;

                PlayerDie();
            }

            PlayerFlash(FlashTime, Time);   //make the player flash after being damaged.

            StartCoroutine(DamageDelay());  //start the coroutine for delay the time reset the state of whether the player can get hurt. 

        }
    }

    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(DamageInterval);   //time of delay the next damage. 

        CanBeDamage = true;
    }

    private void PlayerFlash(int Flashtimes, float Timeperiod)   //make the player flash when get hurt. 
    {
        StartCoroutine(Flashes(Flashtimes, Timeperiod));
    }

    IEnumerator Flashes(int Flashtimes, float Timeperiod)
    {
        int flash = 0;
        while (flash < 2*Flashtimes)   //ensure the player will not be set to disabled when flash ends. 
        {
            PlayerRender.enabled = !PlayerRender.enabled;   //change render between true and false. 
            yield return new WaitForSeconds(Timeperiod);    //Create a short gap of time
            flash++;
        }

        PlayerRender.enabled = true;    //make sure the player will appear normally after flashing. 
    }

    private void PlayerDie()
    {
        PlayerLifeBar.CurrentLife = 0;  //set the life value displayed on life bar to 0. 

        PlayerRb.bodyType = RigidbodyType2D.Static;
        PlayerAnim.SetBool("Isdead", true);   //set the parameter "Isdead" of the player to be true. 

        StartCoroutine(RestartLevel());   //the level will restart after a short period of player dying. 
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))   //The traps like spikes will imediately kills the player once player touch it. 
        {
            PlayerDie();   
        }
    }
}

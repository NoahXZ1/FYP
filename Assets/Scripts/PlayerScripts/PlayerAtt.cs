using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAtt : MonoBehaviour
{
    // Start is called before the first frame update

    private int SwordDamage = 10;  //the value of sword damage. 

    private PolygonCollider2D SwordColl;  
    private Animator PlayerAnim;  //collider of sword and animator

    private float EnableTime = 0.2f;
    private float HideTime = 0.2f;

    [SerializeField] private AudioSource SwordSound;  //the audio of player sword attack.

    void Start()
    {
        PlayerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        SwordColl = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PLayerSwordAtt();  //Check whether the player make an attack by sword.
    }

    private void PLayerSwordAtt()   //Check whether the player make an attack by sword. 
    {
        if (Input.GetButtonDown("SwordAttack"))
        {

            SwordSound.Play();
            PlayerAnim.SetTrigger("SwordAtt");   //trriger the parameter. 

            StartCoroutine(EnableSword());  //Start the Coroutine
        }
    }

    //IEnumerator here is to help control the appearence of sword collider, this will cause damage to enemy only at the frames of animation that the sword is falling down. 
    IEnumerator EnableSword()   //IEnumerator is used to start coroutine, yield can pause coroutine. Used for operations in different frames of animations. 
    {
        yield return new WaitForSeconds(EnableTime);   //Delay the enable of sword collider by EnableTime. 
        SwordColl.enabled = true;    //enable the collider of sword.

        StartCoroutine(HideSword());
    }
    IEnumerator HideSword() {

        yield return new WaitForSeconds(HideTime);   //Delay the hide sword collider by HideTime. 
        SwordColl.enabled = false;   //hide the collider of sword.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))   //the sword will cause damage to enemy when touching them. 
        {
            
            collision.GetComponent<Enemy>().GetDamage(SwordDamage);   //The damage value is the value of SwordDamage. 
           
        }
    }
}

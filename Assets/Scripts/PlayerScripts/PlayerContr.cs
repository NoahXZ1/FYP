using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerContr : MonoBehaviour
{
    // Start is called before the first frame update

    //moving parameter
    private Rigidbody2D PlayerRb;
    private float Horizon;
    public float Walkspeed = 6f; //we can modify the walk speed. 

    private Animator PlayerAnim;

    //jump parameter
    public float Jumpspeed = 10f;
    [SerializeField] private Transform point;
    [SerializeField] private LayerMask Terrian; //terrain is the layer that player can jump. 

    //public int LifeValue;
    [SerializeField] private AudioSource JumpSound;  //the audio of player jump.


    public void Awake() //implement when scripts are loaded. 
    {
        PlayerRb = GetComponent<Rigidbody2D>();  //get the rigidbody of the player. 
        PlayerAnim = GetComponent<Animator>();  //get the animator of player. 
    }

    // Update is called once per frame
    private void Update()
    {
        Horizon = Input.GetAxis("Horizontal"); //get the value of Horizontal which changing from -1 to 1.Determine the moving directions of player. 

        ChangeMovingState(); //switching between idle and walking

        ChangeGroundState();  //change between ground and air state, function "ChangeAirState" will also be called. 

        PlayerJump();//check whether the player will jump. 

    }

    private void FixedUpdate() //Update by a fixed frequency, no relation to frame. 
    {
        PlayerRb.velocity = new Vector2(Horizon * Walkspeed, PlayerRb.velocity.y);  //change the velocity of player. 

        Movedirection();

    }

    private void Movedirection()  //Change the facing direction of player. 
    {
        if (PlayerRb.velocity.x > 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);   //facing right
        }

        else if (PlayerRb.velocity.x < 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0); //rotate to facing left. 
        }
    }

    private void ChangeMovingState()    //determine whether the player is walking or standing
    {
        float moving = Mathf.Abs(Horizon * Walkspeed);

        if (moving > 0f)  //when player is moving
        {
            PlayerAnim.SetBool("Ismoving", true);  //change the value of parameter speed in animator.
        }
        else   //when player stop.
        {
            PlayerAnim.SetBool("Ismoving", false);  //change the value of parameter speed in animator.
        }
    }

    private void ChangeGroundState()  //determine whether the player is on the ground. 
    {
        if (IsOnGround())  //when player is on the ground
        {
            PlayerAnim.SetBool("Isground", true);

            PlayerAnim.SetFloat("YVelocity", 0f); //reset the value of parameter "YVelocity" to initial value 0. 
        }
        else   //when player is in the air (jump or fall)
        {
            PlayerAnim.SetBool("Isground", false);

            ChangeAirState(); //call function ChangeAirState for determining whether player is jumping or falling 
        }
    }

    private void ChangeAirState()  //determine whether the player is jumping or falling
    {

        PlayerAnim.SetFloat("YVelocity", PlayerRb.velocity.y);   //change the value of parameter "YVelocity" according to the velocity.y of current player. The the transition between states can happen. 

    }

    private void PlayerJump()
    {

        if (Input.GetButtonDown("Jump") && IsOnGround())  //player jump when true
        {
            JumpSound.Play();
            PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, Jumpspeed);
        }
    }

    private bool IsOnGround()  //check whether player is touching the ground 
    {
        return Physics2D.OverlapCircle(point.position, .01f, Terrian);
    }


}

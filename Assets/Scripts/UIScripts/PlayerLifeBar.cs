using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeBar : MonoBehaviour
{
    public Text PlayerlifeValue;
    public static int CurrentLife;
    public static int Maxlife;

    private Image LifeBar;  //get the life bar of the player.  
    
    // Start is called before the first frame update
    void Start()
    {
        LifeBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        SetLifePositive();
        
        LifeBar.fillAmount = (float)CurrentLife / (float)Maxlife;  //the life value in PlayerLife script is integer. 

        PlayerlifeValue.text = "Life: " + CurrentLife.ToString() + "/" + Maxlife.ToString();  //set the text of the life bar in UI. 
    }

    private void SetLifePositive()   //make sure the current life will not be negative. 
    {
        if(CurrentLife < 0)
        {
            CurrentLife = 0; 
        }
    }
}

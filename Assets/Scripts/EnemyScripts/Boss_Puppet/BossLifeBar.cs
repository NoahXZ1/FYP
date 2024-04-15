using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifeBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Text BosslifeValue;
    public static int CurrentLife;
    public static int Maxlife;

    private Image LifeBar;  //get the life bar of the boss.  

    // Start is called before the first frame update
    void Start()
    {
        LifeBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        SetLifePositive();

        LifeBar.fillAmount = (float)CurrentLife / (float)Maxlife;  //the life value in BossLife script is integer. 

        BosslifeValue.text = "Life: " + CurrentLife.ToString() + "/" + Maxlife.ToString();  //set the text of the life bar in UI. 
    }

    private void SetLifePositive()   //make sure the current life will not be negative. 
    {
        if (CurrentLife < 0)
        {
            CurrentLife = 0;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBossLevel : MonoBehaviour
{
    // Start is called before the first frame update

    //private bool EvidenceOK = false;     //this valuable can help set the limitation that only when all evidences are collected can player pass the level. 
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && BossLifeBar.CurrentLife == 0)   // only when the player defeat the boss can the player wins the level. 
        {
            Invoke("FinishLevel", 2f);
        }

    }
    
    private void FinishLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

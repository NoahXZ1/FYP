using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EvidenceCollect : MonoBehaviour
{
    private int Evidence = 0; //count the number of evidences collected. 

    [SerializeField] private Text EvidenceNumber;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Evidence"))
        {
            Evidence++;
            Destroy(collision.gameObject);
            EvidenceNumber.text = "Evidence collected: " + Evidence + "/1";   //display the evidence collected situations on the UI. 
        }

        if (collision.gameObject.CompareTag("EndPoint") && Evidence == 1)   //only when the evidence in the level collected can the player enter the next level. 
        {
            Invoke("FinishLevel", 1f);
        }
    }

    private void FinishLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

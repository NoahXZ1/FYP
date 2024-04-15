using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject DialogBox;
    public Text DialogBoxText;
    public string DialogText;
    private bool DialogStart;  //Check whether the dialog box should be displayed or not. 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogStart)
        {
            DialogBoxText.text = DialogText;   //give the text to the dialog box. 
            DialogBox.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)   //when player hit the collider of dialog sign, display the dialog. 
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            DialogStart = true; 

        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //when player exit the collider of dialog sign, disable the dialog.
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            DialogStart = false;
            DialogBox.SetActive(false);

        }
    }
}

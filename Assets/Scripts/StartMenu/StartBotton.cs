using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBotton : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameStart()   //start the game when pressed button.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

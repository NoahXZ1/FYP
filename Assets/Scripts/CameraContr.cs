using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContr : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform player;
    
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);  // Camera follow player.
                                                                                                       // Do not change the value of axis z!
    }
}

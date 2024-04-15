using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotating : MonoBehaviour
{
    private float speed = 1f;
    // Update is called once per frame 
    void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);   //make the saw to rotate. 
    }
}

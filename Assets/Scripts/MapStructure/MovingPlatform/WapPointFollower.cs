using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WapPointFollower : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject[] EndPoint;  //the two end points of the moving platform. 
    private int CurrentEndPoint = 0;

    [SerializeField] private float speed = 3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(EndPoint[CurrentEndPoint].transform.position, transform.position) < .1f)   //the platform will moving towards the other side when reaching the endpoint. 
        {
            CurrentEndPoint++;
 
            if (CurrentEndPoint >= EndPoint.Length)  //set to waypoint 1 when reach the last waypoint.
            {
                CurrentEndPoint = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, EndPoint[CurrentEndPoint].transform.position, Time.deltaTime * speed);  //make the platform start moving. 
    }
}

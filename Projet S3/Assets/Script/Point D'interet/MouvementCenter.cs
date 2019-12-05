using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementCenter : MonoBehaviour
{
    public bool activeMouvement = false;
    public GameObject[] waypoint;
    public float speed;
    public int currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeMouvement)
        {
            if (Vector3.Distance(transform.position, waypoint[currentWaypoint].transform.position) > 1.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint[currentWaypoint].transform.position, speed*Time.deltaTime);
            }
            else
            {
                
                if(currentWaypoint==waypoint.Length-1)
                {
                    currentWaypoint = 0;
                }
                else
                {
                    currentWaypoint++;
                }
            }
        }
        
    }
}

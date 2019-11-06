using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{
    public Transform otherPlayer;
    public bool trigSkill = false;
    public bool isArrived = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigSkill)
        {
            trigSkill = false;
            isArrived = false;
        }
        if(!isArrived)
        {
            AttractSkill();
            if(Vector3.Distance(transform.position, otherPlayer.position) < 1f)
            {
                isArrived = true;
            }
        }
    }

    public void AttractSkill()
    {
        transform.position = Vector3.MoveTowards(transform.position, otherPlayer.position, 10f * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiBehavior : MonoBehaviour
{

   public GameObject target;
    public float speed;
    public bool isOnSlam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOnSlam)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

    }
}

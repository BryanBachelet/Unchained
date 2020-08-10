using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLerp : MonoBehaviour
{
    public GameObject target;
    public float speedOfMouvement;
    private float compteur;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 0.01f)
        {
            Vector3 dir = target.transform.position - transform.position;
            transform.position += dir.normalized * speedOfMouvement *Time.deltaTime; 
           
        }
    }
}

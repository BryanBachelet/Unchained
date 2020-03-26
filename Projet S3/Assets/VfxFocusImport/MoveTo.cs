using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public GameObject focusPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //focusPoint = GameObject.FindGameObjectWithTag("focus");

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(focusPoint.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, focusPoint.transform.position, speed * Time.deltaTime);
    }
}

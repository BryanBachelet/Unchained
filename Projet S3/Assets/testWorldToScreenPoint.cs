using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testWorldToScreenPoint : MonoBehaviour
{
    public Vector3 testpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        testpos = Camera.main.WorldToScreenPoint(transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitesseFunc : MonoBehaviour
{
    public bool add;
    static public float velocity;
    public float ratioTransmit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeVelocity(add, velocity,  ratioTransmit);
        }
    }

    public (float, float) changeVelocity(bool isAdd, float currentVelocity, float ratio)
    {
        if(isAdd)
        {
            velocity = currentVelocity * (1 + ratio);
            ratio *= 0.9f;
        }
        else
        {
            velocity = currentVelocity * (1 - ratio);
            ratio *= 1.1f;
        }
        return (velocity, ratio);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRangeFocus : MonoBehaviour
{
    public ParticleSystemForceField myPSFF;
    public float min;
    public float minMax;
    public float minCur;
    public float max;
    public float maxCur;
    public float maxMax;
    bool isUp;
    bool isUp2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isUp)
        {
            if(minCur < minMax)
            {
                minCur += Time.deltaTime;
            }
            else
            {
                isUp = false;
            }
        }
        else if (!isUp)
        {
            if (minCur > min)
            {
                minCur -= Time.deltaTime;
            }
            else
            {
                isUp = true;
            }
        }
        if (isUp2)
        {
            if (maxCur < maxMax)
            {
                maxCur += Time.deltaTime;
            }
            else
            {
                isUp2 = false;
            }
        }
        else if (!isUp2)
        {
            if (maxCur > max)
            {
                maxCur -= Time.deltaTime;
            }
            else
            {
                isUp2 = true;
            }
        }
        myPSFF.endRange = maxCur;
        myPSFF.startRange = minCur;
    }
}

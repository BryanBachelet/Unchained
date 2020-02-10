using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleEcharpe : MonoBehaviour
{
    public float scaleMax;
    public Velocity myVeloScript;
    float valeurlocalScaleYInit;
    public int echarpeLink; // 1= blue, 2= orange, 3=violet
    // Start is called before the first frame update
    void Start()
    {
        valeurlocalScaleYInit = transform.parent.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(echarpeLink == 1)
        {
            transform.localScale = new Vector3(transform.localScale.x, myVeloScript.velocityStatOne * valeurlocalScaleYInit / 2000 , transform.localScale.z);
        }
        if (echarpeLink == 2)
        {
            transform.localScale = new Vector3(transform.localScale.x, myVeloScript.velocityStatTwo * valeurlocalScaleYInit / 2000, transform.localScale.z);
        }
        if (echarpeLink == 3)
        {
            transform.localScale = new Vector3(transform.localScale.x, myVeloScript.velocityStatThree * valeurlocalScaleYInit / 2000, transform.localScale.z);
        }
    }
}

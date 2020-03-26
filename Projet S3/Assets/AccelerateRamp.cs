using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateRamp : MonoBehaviour
{
    public float maxSpeed = 6f;
    public float timeZeroToMax = 2.5f;
    float accelRatePerSec;
    float forwardVelocity;

    // Start is called before the first frame update
    private void Awake()
    {
        accelRatePerSec = maxSpeed / timeZeroToMax;
        forwardVelocity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            forwardVelocity += accelRatePerSec * Time.deltaTime;
            forwardVelocity = Mathf.Min(forwardVelocity, maxSpeed);
        }


    }
}

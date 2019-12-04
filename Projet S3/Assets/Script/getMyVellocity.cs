using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getMyVellocity : MonoBehaviour
{
    Rigidbody myRB;
    Vector3 myVelocity;
    public AmplifyMotionEffect myAME;
    // Start is called before the first frame update
    void Start()
    {
        myRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        myVelocity = myRB.velocity;
        Debug.Log(myVelocity.magnitude);
        Debug.Log((3 * myVelocity.magnitude) / 70);
        myAME.MaxVelocity = (3 * myVelocity.magnitude / 70);
    }
}

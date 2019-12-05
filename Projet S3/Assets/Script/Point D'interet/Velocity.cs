using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public float velocityStatOne;
    public float velocityStatTwo;
    public float velocityStatThree;

    public float DecreaseOfVelocity;

    private float compteurOne;
    private float compteurTwo;
    private float compteurThree;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAddVelocityPoint(int i)
    {
        switch (i)
        {
            case 0:
                Debug.Log("1");
                velocityStatOne++;
                compteurOne = 0;

                break;
            case 1:

                velocityStatTwo++;
                compteurTwo = 0;

                break;
            case 2:

                velocityStatThree++;
                compteurThree = 0;

                break;

        }
    }
}

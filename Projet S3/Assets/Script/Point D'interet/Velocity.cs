using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    [Header("Projection Strengh Gain")]
    public float velocityStatOne;
    public float convertisseurStatOne = 10;
    public float realPointOne;

    [Header("V2")]
    public float velocityStatTwo;
    public float convertisseurStatTwo = 10;
    public float realPointTwo;

    [Header("V3")]
    public float velocityStatThree = 0;
    public float convertisseurStatThree = 10;
    public float realPointThree ;

    [Header("General Stat")]
    public float DecreaseOfTimerVelocity = 2;
    public float DecreaseOfVelocity = 1;

    private float compteurOne;
    private float compteurTwo;
    private float compteurThree;

    private EnnemiStock ennemiStock;
    private PlayerMoveAlone playerMove;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMoveAlone>();
        ennemiStock = GetComponent<EnnemiStock>();
    }

    // Update is called once per frame
    void Update()
    {

        realPointOne = velocityStatOne / convertisseurStatOne;
        ennemiStock.powerOfProjection = playerMove.powerOfProjection + realPointOne;
        Debug.Log(playerMove.powerOfProjection + realPointOne);
        realPointTwo = velocityStatTwo / convertisseurStatTwo;
        realPointThree = velocityStatThree / convertisseurStatThree;

        if (compteurOne > DecreaseOfTimerVelocity)
        {
            velocityStatOne -= DecreaseOfVelocity * Time.deltaTime;
        }
        else
        {
            compteurOne += Time.deltaTime;
        }
        if (compteurTwo > DecreaseOfTimerVelocity)
        {
            velocityStatTwo -= DecreaseOfVelocity * Time.deltaTime;
        }
        else
        {
            compteurTwo += Time.deltaTime;
        }
        if (compteurThree > DecreaseOfTimerVelocity)
        {
            velocityStatThree -= DecreaseOfVelocity * Time.deltaTime;
        }
        else
        {
            compteurThree += Time.deltaTime;
        }

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

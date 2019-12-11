using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    public Slider projectionSlider;
    public Slider v2Slider;
    public Slider v3Slider;
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

        realPointTwo = velocityStatTwo / convertisseurStatTwo;
        realPointThree = velocityStatThree / convertisseurStatThree;

        if (compteurOne > DecreaseOfTimerVelocity && velocityStatOne > 0)
        {
            velocityStatOne -= DecreaseOfVelocity * Time.deltaTime;
        }
        else
        {
            compteurOne += Time.deltaTime;
        }
        if (compteurTwo > DecreaseOfTimerVelocity && velocityStatTwo > 0)
        {
            velocityStatTwo -= DecreaseOfVelocity * Time.deltaTime;
        }
        else
        {
            compteurTwo += Time.deltaTime;
        }
        if (compteurThree > DecreaseOfTimerVelocity && velocityStatThree > 0)
        {
            velocityStatThree -= DecreaseOfVelocity * Time.deltaTime;
        }
        else
        {
            compteurThree += Time.deltaTime;
        }

        if (projectionSlider != null) projectionSlider.value = realPointOne / 100;
        if (v2Slider != null) v2Slider.value = realPointTwo / 100;
        if (v3Slider != null) v3Slider.value = realPointThree / 100;
    }

    public void GetAddVelocityPoint(int i)
    {
        switch (i)
        {
            case 0:

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

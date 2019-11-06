using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourbillon : MonoBehaviour
{
    public Transform otherPlayer;
    public bool trigSkill = false;
    public bool isArrived = true;
    public float angleToRotate;
    public float angleRotated;
    public float angleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (trigSkill)
        {
            angleRotated = 0;
            angleToRotate = 360 + (72 * VitesseFunction.currentLv);
            trigSkill = false;
            isArrived = false;
        }
        if (!isArrived)
        {
            AttractSkill();
            if (angleRotated >= angleToRotate)
            {
                isArrived = true;
            }
        }
    }

    public void AttractSkill()
    {
        angleRotated += angleSpeed * Time.deltaTime;
        transform.RotateAround(otherPlayer.position, Vector3.up, angleSpeed * Time.deltaTime);
    }
}


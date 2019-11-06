using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMSkill : MonoBehaviour
{
    public Transform midPointOtherPlayer;
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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (trigSkill)
        {
            angleRotated = 0;
            trigSkill = false;
            isArrived = false;
        }
        if (!isArrived)
        {
            SauteMSkill();
            if (angleRotated >= angleToRotate)
            {
                isArrived = true;
            }
        }
    }

    public void SauteMSkill()
    {
        angleRotated += angleSpeed * Time.deltaTime;
        transform.RotateAround(midPointOtherPlayer.position, Vector3.back, angleSpeed * Time.deltaTime);
    }
}

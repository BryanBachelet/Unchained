using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl1 : MonoBehaviour
{
    private Rigidbody rigid;
    public int playerManette = 1;
    public float horizontal;
    public float vertical;
    public float deadzoneManette;
    [Header("Speed")] public float maxSpeed;
    public float mediumSpeed;
    public float highSpeed;
    public float speed;
    public float speedMin;
    public float acceleration;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal" + playerManette.ToString());
        vertical = Input.GetAxis("Vertical" + playerManette.ToString());

        if (horizontal < deadzoneManette && horizontal > -deadzoneManette)
        {
            horizontal = 0;
        }
        if (horizontal > deadzoneManette)
        {
            horizontal = 1;
        }
        if (horizontal < -deadzoneManette)
        {
            horizontal = -1;
        }
        if (vertical < deadzoneManette && vertical > -deadzoneManette)
        {
            vertical = 0;
        }
        if (vertical > deadzoneManette)
        {
            vertical = 1;
        }
        if (vertical < -deadzoneManette)
        {
            vertical = -1;
        }

        if (vertical != 0 || horizontal != 0)
        {
            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
        }
        if(vertical == 0 && horizontal == 0)
        {
            if (speed > 0)
            {
                speed -= acceleration * Time.deltaTime;
            }
           
        }
        rigid.MovePosition(rigid.position + new Vector3(horizontal * (speedMin + speed) * Time.deltaTime, 0, vertical * (speedMin + speed) * Time.deltaTime));


    }
}

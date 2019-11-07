using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMSkill : MonoBehaviour
{

    private bool isJumping = false;
    private float angleToRotate;
    private float angleRotated;
    public float angleSpeed;
    public float ratioAugmented = 0.05f;
    public float distanceMinimum = 0.5f;

    private PlayerNumber playerNumber;
    private Vector3 pos;
    private Vector3 normal;
    private float Dist;
    private Vector3 dir;

    void Awake()
    {
        playerNumber = GetComponent<PlayerNumber>();

    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (Input.GetKeyDown("joystick " + playerNumber.manetteNumber.ToString() + " button 5") && !isJumping)
        {
            normal = Vector3.Cross(PlayerCommands.DirectionBetweenPlayer(), Vector3.up);
            angleRotated = 0;
            isJumping = true;
            PointPivot();
        }
        if (isJumping)
        {
            SauteMSkill();
            if (angleRotated >= angleToRotate)
            {
                isJumping = false;
            }
        }
    }

    public void SauteMSkill()
    {
        angleRotated += angleSpeed * Time.deltaTime;
        transform.RotateAround(pos, normal, angleSpeed * Time.deltaTime);
    }
    public void PointPivot()
    {
        dir = PlayerCommands.DirectionBetweenPlayer();
        Dist = Vector3.Distance(PlayerCommands.player1.transform.position, PlayerCommands.player2.transform.position);
        pos = PlayerCommands.player1.transform.position + (dir.normalized * (Dist * (distanceMinimum + VitesseFunction.RatioAugmented(ratioAugmented))));
        pos = new Vector3(pos.x, 0, pos.z);
    }
}

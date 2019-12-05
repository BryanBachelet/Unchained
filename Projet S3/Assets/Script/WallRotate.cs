using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotate : MonoBehaviour
{
    public bool hasHitWall;
    public bool isOnrotate;
    public Vector3 rotationPoint;
    public Klak.Motion.SmoothFollow mySmoothFollow;

    private RotationPlayer rotationPlayer;
    public GameObject hitGOPos;
    // Start is called before the first frame update
    void Start()
    {
        rotationPlayer = GetComponent<RotationPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Attract1");
        if (hasHitWall)
        {
            if (Input.GetKey(KeyCode.Mouse0) || input < 0)
            {
                isOnrotate = rotationPlayer.StartRotationWall(gameObject, rotationPoint, false);
                mySmoothFollow.target = hitGOPos.transform;
            }

            if (Input.GetKey(KeyCode.Mouse0) || input > 0)
            {
                isOnrotate = rotationPlayer.StartRotationWall(gameObject, rotationPoint, true);
                mySmoothFollow.target = hitGOPos.transform;
            }

            if (!Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0) && input == 0)
            {
                mySmoothFollow.target = null;
                rotationPlayer.StopRotation(false);
                hasHitWall = false;
            }

        }
    }
}

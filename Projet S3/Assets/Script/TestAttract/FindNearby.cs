using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Klak.Motion;

public class FindNearby : MonoBehaviour
{
    public Collider[] GrabWall;
    public LayerMask maskWall;
    public float radius;
    Vector3 distanceNear;
    public Transform NearestObject;
    public LineRenderer myLR;
    private PlayerNumber playerNumber;
    public AddForce myAF;
    public float angleSpeed = 180;
    private float angleRotated;
    bool trigGrab = false;

    public Gravity myGrav;
    private string playerIdentity;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        playerIdentity = "Player" + playerNumber.playerNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        myLR.SetPosition(0, transform.position);

        GrabWall = Physics.OverlapSphere(transform.position, radius, maskWall);

        
        if (Input.GetKeyDown(KeyCode.Alpha4) && playerIdentity == "Player1")
        {
            myGrav.gravityForce = 0;
            if(GrabWall.Length > 1)
            {
                for (int i = 0; i < GrabWall.Length; i++)
                {
                    if (distanceNear == Vector3.zero)
                    {
                        distanceNear = GrabWall[i].transform.position - transform.position;
                        NearestObject = GrabWall[i].transform;
                        myLR.SetPosition(1, GrabWall[i].transform.position);
                    }
                    if (Vector3.Distance(GrabWall[i].transform.position, transform.position) < Vector3.Distance(NearestObject.position, transform.position))
                    {
                        distanceNear = GrabWall[i].transform.position;
                        NearestObject = GrabWall[i].transform;
                        myLR.SetPosition(1, GrabWall[i].transform.position);
                    }
                }

            }
            NearestObject.gameObject.AddComponent<SmoothFollow>();
            myAF.Expluse(NearestObject.gameObject);
            trigGrab = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) && playerIdentity == "Player2")
        {
            myGrav.gravityForce = 0;
            if (GrabWall.Length > 1)
            {
                for (int i = 0; i < GrabWall.Length; i++)
                {
                    if (distanceNear == Vector3.zero)
                    {
                        distanceNear = GrabWall[i].transform.position - transform.position;
                        NearestObject = GrabWall[i].transform;
                        myLR.SetPosition(1, GrabWall[i].transform.position);
                    }
                    if (Vector3.Distance(GrabWall[i].transform.position, transform.position) < Vector3.Distance(NearestObject.position, transform.position))
                    {
                        distanceNear = GrabWall[i].transform.position;
                        NearestObject = GrabWall[i].transform;
                        myLR.SetPosition(1, GrabWall[i].transform.position);
                    }
                }
            }

            trigGrab = true;
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            myGrav.gravityForce = 10;
            trigGrab = false;
            angleRotated = 0;
            if(NearestObject != null)
            {
                NearestObject.GetComponent<SmoothFollow>().target = null;
                distanceNear = Vector3.zero;
                NearestObject = null;
            }


        }
        if (trigGrab)
        {
            GrabSomething();
        }
        else
        {

        }


    }

    public void GrabSomething()
    {
        if(NearestObject != null)
        {
            NearestObject.GetComponent<SmoothFollow>().target = transform;
            angleRotated += angleSpeed * Time.deltaTime;
            transform.RotateAround(NearestObject.position, Vector3.up, angleSpeed * Time.deltaTime);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamPlayer : MonoBehaviour
{
    public float angleSpeed;
    public float fallSpeed;
    public GameObject slamAOEPrefab;
    public LayerMask groundLayer;

    private GameObject pointPivot;
    private GameObject objectSlam;
    private Vector3 dir;
    private Vector3 normal;
    private Vector3 startPosPivot, startPosObjectSlam;
    private float angleCompteur;
    private bool slam, onDrop;

    private int i;
    private EnnemiStock ennemiStock;

    private void Start()
    {
        ennemiStock = GetComponent<EnnemiStock>();
    }


    // Update is called once per frame
    void Update()
    {
        if (slam)
        {
            if (!onDrop)
            {
                angleCompteur += Mathf.Abs(angleSpeed) * Time.deltaTime;
                objectSlam.transform.RotateAround(startPosPivot, normal, -angleSpeed * Time.deltaTime);

                if (angleCompteur > 80)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(objectSlam.transform.position, -Vector3.up, out hit, Mathf.Infinity, groundLayer))
                    {
                        float dist = Vector3.Distance(hit.point, objectSlam.transform.position);
                        if (dist <2f)
                        {

                            StopSlam();
                            objectSlam.transform.position = new Vector3(objectSlam.transform.position.x, 1, objectSlam.transform.position.z);
                        }

                    }

                }
                i++;
                if (i > 3)
                {
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.E))
                    {
                        onDrop = true;
                     
                    }
                }
            }
            else
            {
                JetSlam();
            }
        }
    }

    public bool StartSlam(GameObject slamObject, GameObject PointPivot)
    {
        objectSlam = slamObject;
        pointPivot = PointPivot;
        if (objectSlam.GetComponent<EnnemiDestroy>())
        {
            objectSlam.GetComponent<EnnemiDestroy>().isDestroying = false;
        }
        if (pointPivot.GetComponent<EnnemiDestroy>())
        {
            pointPivot.GetComponent<EnnemiDestroy>().isDestroying = false;
        }
        StartSetUp();
        return slam = true;
    }

    public void StartSetUp()
    {
        dir = objectSlam.transform.position - pointPivot.transform.position;
        normal = Vector3.Cross(-dir, Vector3.up);
        startPosObjectSlam = objectSlam.transform.position;
        startPosPivot = pointPivot.transform.position;
    }

    public void JetSlam()
    {
        objectSlam.transform.position = Vector3.MoveTowards(objectSlam.transform.position, new Vector3(objectSlam.transform.position.x, 1, objectSlam.transform.position.z), fallSpeed * Time.deltaTime);
        if (Vector3.Distance(objectSlam.transform.position, new Vector3(objectSlam.transform.position.x, 1, objectSlam.transform.position.z)) < 1.5f)
        {
            StopSlam();
        }
    }

    public void StopSlam()
    {
        Instantiate(slamAOEPrefab, objectSlam.transform.position, transform.rotation);
        CheckEnnnemi();
        objectSlam.transform.position = new Vector3(objectSlam.transform.position.x, 1, objectSlam.transform.position.z);
        slam = false;
        onDrop = false;
        angleCompteur = 0;
        ennemiStock.StopSlam();
        i = 0;
    }
    private void CheckEnnnemi()
    {

        if (objectSlam.GetComponent<EnnemiDestroy>())
        {
            objectSlam.GetComponent<EnnemiDestroy>().isDestroying = true;
        }
        if (pointPivot.GetComponent<EnnemiDestroy>())
        {
            pointPivot.GetComponent<EnnemiDestroy>().isDestroying = true;
        }

    }

}




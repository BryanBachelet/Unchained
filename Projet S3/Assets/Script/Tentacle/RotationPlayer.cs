using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    public bool rotate = false;
    public float angleSpeed;
    public float angleMax;

    private float angleCompteur;
    private float currentAngleMax;

    private EnnemiStock stocks;
    private float forceOfSortie;
    private GameObject objectToRotate;
    private GameObject pointPivot;
    private string tagEnter;
    private Vector3 previousPos;
    public bool changeSens;
    private int i;
    private void Start()
    {
        stocks = GetComponent<EnnemiStock>();
        currentAngleMax = angleMax;
    }

    public void Update()
    {
        if (rotate)
        {
            if (objectToRotate != null)
            {
                previousPos = objectToRotate.transform.position;
                angleCompteur += Mathf.Abs(angleSpeed) * Time.deltaTime;
                objectToRotate.transform.RotateAround(pointPivot.transform.position, Vector3.up, angleSpeed * Time.deltaTime);
                i++;
                if (i > 3)
                {
                    if (Input.GetMouseButtonDown(0) && !changeSens || Input.GetMouseButtonDown(1) && !changeSens)
                    {
                        ChangeRotationDirection();

                    }
                }
                if (angleCompteur > currentAngleMax)
                {
                    StopRotation(previousPos, tagEnter);
                }
            }
        }
    }

    public void ChangeRotationDirection()
    {
        angleSpeed = -angleSpeed;
        currentAngleMax = angleMax + angleCompteur;
        angleCompteur = 0;
        changeSens = true;
    }

    public bool StartRotation(GameObject objetRotate, GameObject positionPivot, string tag, float forceSortie)
    {
        objectToRotate = objetRotate;
        pointPivot = positionPivot;
        tagEnter = tag;
        forceOfSortie = forceSortie;
        changeSens = false;
        i = 0;
        return rotate = true;
    }

    public void StopRotation(Vector3 previousPos, string tag)
    {
        Vector3 newDir = objectToRotate.transform.position - previousPos;
        objectToRotate.tag = tag;
        objectToRotate.GetComponent<Rigidbody>().AddForce(newDir.normalized * forceOfSortie, ForceMode.Impulse);
        CheckEnnnemi();
        objectToRotate = null;
        currentAngleMax = angleMax;
        angleCompteur = 0;
        rotate = false;
        stocks.StopRotate();
    }

    private void CheckEnnnemi()
    {
        Vector3 newDir = objectToRotate.transform.position - previousPos;
        if (objectToRotate.GetComponent<EnnemiDestroy>())
        {
            objectToRotate.GetComponent<EnnemiDestroy>().isDestroying = true;
        }
        else
        {
            pointPivot.GetComponent<Rigidbody>().AddForce(Vector3.up * forceOfSortie * 10, ForceMode.Impulse);
            objectToRotate.GetComponent<Rigidbody>().AddForce(newDir.normalized * forceOfSortie * 10, ForceMode.Impulse);
        }
    }
}

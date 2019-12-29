using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    public float angleSpeed;
    public float angleMax;

    private float angleCompteur;
    private float currentAngleMax;

    public Material lineMat;
    private EnnemiStock stocks;
    private float forceOfSortie;
    private GameObject objectToRotate;
    private GameObject gameObjectPointPivot;
    private Vector3 pointPivot;
    private string tagEnter;
    public Vector3 previousPos;
    public bool rotate = false;
    private bool changeSens;
    private int i;
    public LineRenderer lineRenderer;
    private LineRend line;
    public Vector3 newDir;
    public GameObject vfxShockWave;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        line = GetComponentInChildren<LineRend>();
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
                objectToRotate.transform.RotateAround(pointPivot, Vector3.up, angleSpeed * Time.deltaTime);
                newDir = objectToRotate.transform.position - previousPos;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, pointPivot);
                line.p1 = transform.position;
                line.p2 = stocks.ennemiStock.transform.position;
                line.ColliderSize();
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
                    angleCompteur = 0;
                }
            }
        }
        else
        {
            previousPos = transform.position;
        }
    }

    public void ChangeRotationDirection()
    {
        angleSpeed = -angleSpeed;
        currentAngleMax = angleMax + angleCompteur;
        angleCompteur = 0;
        changeSens = true;
    }

    public bool StartRotation(GameObject objetRotate, GameObject positionPivot, string tag, float forceSortie, bool changeRotate)
    {
        objectToRotate = objetRotate;
        gameObjectPointPivot = positionPivot;
        pointPivot = positionPivot.transform.position;
        tagEnter = tag;
        forceOfSortie = forceSortie;
        changeSens = false;
        i = 0;
        //previousPos = objetRotate.transform.position;
        if (changeRotate)
        {
            if (angleSpeed > 0)
            {
                angleSpeed = -angleSpeed;
            }
        }
        if (!changeRotate)
        {
            angleSpeed = Mathf.Abs(angleSpeed);
        }
        return rotate = true;
    }

    public bool StartRotationWall(GameObject objetRotate, Vector3 positionPivotWall, float forceSortie, bool changeRotate)
    {
        objectToRotate = objetRotate;
        pointPivot = positionPivotWall;
        forceOfSortie = forceSortie;
        changeSens = false;
        i = 0;
        // previousPos = objetRotate.transform.position;
        if (changeRotate)
        {
            if (angleSpeed > 0)
            {
                angleSpeed = -angleSpeed;
            }
        }
        if (!changeRotate)
        {
            angleSpeed = Mathf.Abs(angleSpeed);
        }
        return rotate = true;
    }

    public void StopRotation(bool isWall)
    {
        if (isWall)
        {
            transform.tag = tagEnter;
        }

        CheckEnnnemi(isWall);
        transform.GetComponent<Rigidbody>().AddForce(newDir.normalized * forceOfSortie, ForceMode.Impulse);
        transform.GetComponent<WallRotate>().hasHitWall = false;
        if (vfxShockWave != null)
        {
            newDir = transform.transform.position - previousPos;
            float angleToRotate = Vector3.SignedAngle(transform.forward, newDir.normalized, Vector3.up);

            GameObject vfxSW = Instantiate(vfxShockWave, transform.position, Quaternion.Euler(0, angleToRotate, 0));


        }
        objectToRotate = null;
        currentAngleMax = angleMax;
        angleCompteur = 0;
        stocks.StopRotate();
        rotate = false;
    }

    private void CheckEnnnemi(bool isEnnemi)
    {

        newDir = transform.position - previousPos;
        if (gameObjectPointPivot != null)
        {
            gameObjectPointPivot.GetComponent<Rigidbody>().AddForce(Vector3.up * forceOfSortie, ForceMode.Impulse);
        }
        if (transform.GetComponent<PlayerMoveAlone>())
        {
            transform.GetComponent<PlayerMoveAlone>().DirProjection = newDir;
            transform.GetComponent<PlayerMoveAlone>().powerProjec = forceOfSortie;
        }

    }


    private void OnRenderObject()
    {
        if (Camera.current.name == "Camera")
        {
            if (stocks.ennemiStock != null)
            {
                GL.Begin(GL.LINES);
                lineMat.SetPass(0);

                GL.Color(Color.blue);
                GL.Vertex(transform.position);
                GL.Vertex(transform.position + (newDir).normalized * 100);
                GL.End();
            }

        }

    }
}

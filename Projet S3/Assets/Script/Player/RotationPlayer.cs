﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlayer : MonoBehaviour
{

    public float angleSpeed;
    public float currentSpeed;
    float angleSpeedIni;
    public AnimationCurve accelerationValue;
    public float angleMax;
    [Header("Options")]
    public bool limitationLongeur;
    public float limitLongeur;
    float limitLongeurMin = 20;
    public float speedOfDeplacement= 5;
    private float angleCompteur;
    private float currentAngleMax;

    public Material lineMat;
    private EnnemiStock stocks;
    private float forceOfSortie;

    private GameObject gameObjectPointPivot;
    private Vector3 pointPivot;
    private string tagEnter;

    public bool rotate = false;
    private bool changeSens;
    private int i;
    private LineRenderer lineRenderer;
    private LineRend line;
  [Range(0, 1)] public float predictionMvtRotate = 0.5f;
    [HideInInspector] public Vector3 newDir;
    [HideInInspector] public Vector3 nextDir;
    private GameObject Chara;
    [HideInInspector] public float angleAvatar = 0;

    private float speedRotationAnim = 70;
    public GameObject vfxShockWave;

    private Rigidbody playerRigid;
    private PlayerMoveAlone moveAlone;

    [FMODUnity.EventRef]
    public string rotation;
    private FMOD.Studio.EventInstance rotationSound;
    bool checksound = false;

    public float tempsEcouleAcceleration;
    public float tempsAcceleration;

    int checkSensRotation;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        moveAlone = GetComponent<PlayerMoveAlone>();
        forceOfSortie = moveAlone.powerOfProjection;
        line = GetComponentInChildren<LineRend>();
        stocks = GetComponent<EnnemiStock>();
        playerRigid = GetComponent<Rigidbody>();
        currentAngleMax = angleMax;
        rotationSound = FMODUnity.RuntimeManager.CreateInstance(rotation);
        rotationSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        angleSpeedIni = angleSpeed;
    }

    public void FixedUpdate()
    {

        if (rotate)
        {
            if(tempsAcceleration > tempsEcouleAcceleration)
            {
                tempsEcouleAcceleration += Time.deltaTime;
            }
            if(!checksound)
            {
                rotationSound.start();
                checksound = true;
            }
            playerRigid.velocity = Vector3.zero;

            if (tagEnter == tag)
            {
                pointPivot = stocks.ennemiStock.transform.position;
            }

            angleSpeed = accelerationValue.Evaluate(tempsEcouleAcceleration) * checkSensRotation;
            angleCompteur += Mathf.Abs(angleSpeed) * Time.deltaTime;
            transform.RotateAround(pointPivot, Vector3.up, angleSpeed * Time.deltaTime);

            if (limitationLongeur)
            {
                if (GetDistance() > limitLongeur)
                {
                    Vector3 dir = (transform.position - pointPivot).normalized;
                    Vector3 posToGo = pointPivot + dir * limitLongeur;
                    transform.position = Vector3.Lerp(transform.position, posToGo, speedOfDeplacement * Time.deltaTime);
                }
                if (GetDistance() < limitLongeurMin)
                {
                    Vector3 dir = (transform.position - pointPivot).normalized;
                    Vector3 posToGo = pointPivot + dir * limitLongeurMin;
                    transform.position = Vector3.Lerp(transform.position, posToGo, speedOfDeplacement * Time.deltaTime);
                }
            }
            angleAvatar += speedRotationAnim * Time.deltaTime;   /* Vector3.SignedAngle(Vector3.forward, GetDirection(), Vector3.up);*/
            //Chara.transform.eulerAngles = new Vector3(0, angleAvatar, 0);
            float angle = Vector3.SignedAngle(Vector3.forward, GetDirection(), Vector3.up);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, pointPivot);
            line.p1 = transform.position;
            line.p2 = stocks.ennemiStock.transform.position;
            line.ColliderSize();

            if (angleCompteur > currentAngleMax)
            {
                angleCompteur = 0;
            }

            GetNextDirection();
        }
        else
        {
            if(checksound)
            {
                rotationSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                checksound = false;
            }
            angleAvatar = 0;
        }
    }

  
    public bool StartRotation(GameObject objetRotate, GameObject positionPivot, string tag, float forceSortie, bool changeRotate)
    {
        tagEnter = null;
        gameObjectPointPivot = positionPivot;
        pointPivot = positionPivot.transform.position;
        tagEnter = tag;
        forceOfSortie = forceSortie;
        changeSens = false;
        i = 0;

        if (changeRotate)
        {
            if (angleSpeed > 0)
            {
                angleSpeed = -angleSpeed;
                checkSensRotation = -1;
            }
        }
        if (!changeRotate)
        {
            angleSpeed = Mathf.Abs(angleSpeed);
            checkSensRotation = 1;
        }
        return rotate = true;
    }

    public bool StartRotationWall(GameObject objetRotate, Vector3 positionPivotWall, GameObject positionPivot, float forceSortie, bool changeRotate)
    {
        tagEnter = null;
        gameObjectPointPivot = positionPivot;
        pointPivot = positionPivotWall;
        forceOfSortie = forceSortie;
        changeSens = false;
        i = 0;

        if (changeRotate)
        {
            if (angleSpeed > 0)
            {
                angleSpeed = -angleSpeed;
                checkSensRotation = -1;
            }
        }
        if (!changeRotate)
        {
            angleSpeed = Mathf.Abs(angleSpeed);
            checkSensRotation = 1;
        }
        return rotate = true;
    }

    public void StopRotation(bool isWall)
    {
        if (StateAnim.state == StateAnim.CurrentState.Rotate)
        {
            StateAnim.ChangeState(StateAnim.CurrentState.Projection);
        }
        if (isWall)
        {
            transform.tag = tagEnter;
        }

        CheckEnnnemi(isWall, angleSpeed);
       
        playerRigid.AddForce(newDir.normalized * forceOfSortie, ForceMode.Impulse);
        transform.GetComponent<WallRotate>().hasHitWall = false;
        if (vfxShockWave != null)
        {

            float angleConversion = transform.eulerAngles.y;
            angleConversion = angleConversion > 180 ? angleConversion - 360 : angleConversion;
            float angleAvatar = Vector3.SignedAngle(Vector3.forward, newDir.normalized, Vector3.up);
            if (angleAvatar > 0 && angleAvatar < 90)
            {
                angleAvatar = 180 - angleAvatar;
            }
            if (angleAvatar < 0 && angleAvatar > -90)
            {
                angleAvatar = -180 - angleAvatar;
            }


            if (angleConversion < 0 && angleAvatar == 180)
            {
                angleAvatar = -180;
            }



            GameObject vfxSW = Instantiate(vfxShockWave, transform.position, Quaternion.Euler(0, angleAvatar, 0));


        }

        currentAngleMax = angleMax;
        angleCompteur = 0;
        stocks.StopRotate();
        tempsEcouleAcceleration = 0;
        rotate = false;

    }
  public void StopRotation(Vector3 dir)
    {
        if (StateAnim.state == StateAnim.CurrentState.Rotate)
        {
            StateAnim.ChangeState(StateAnim.CurrentState.Projection);
        }
        if (false)
        {
            transform.tag = tagEnter;
        }

        CheckEnnnemi(false, angleSpeed, dir);
       
       dir = new Vector3(dir.x,0,dir.z);
        playerRigid.AddForce(dir.normalized * forceOfSortie, ForceMode.Impulse);
        playerRigid.AddForce(-Vector3.up.normalized * 50, ForceMode.Impulse);
        transform.GetComponent<WallRotate>().hasHitWall = false;
        if (vfxShockWave != null)
        {

            float angleConversion = transform.eulerAngles.y;
            angleConversion = angleConversion > 180 ? angleConversion - 360 : angleConversion;
            float angleAvatar = Vector3.SignedAngle(Vector3.forward, newDir.normalized, Vector3.up);
            if (angleAvatar > 0 && angleAvatar < 90)
            {
                angleAvatar = 180 - angleAvatar;
            }
            if (angleAvatar < 0 && angleAvatar > -90)
            {
                angleAvatar = -180 - angleAvatar;
            }


            if (angleConversion < 0 && angleAvatar == 180)
            {
                angleAvatar = -180;
            }



            GameObject vfxSW = Instantiate(vfxShockWave, transform.position, Quaternion.Euler(0, angleAvatar, 0));


        }

        currentAngleMax = angleMax;
        angleCompteur = 0;
        stocks.StopRotate();
        tempsEcouleAcceleration = 0;
        rotate = false;

    }
public void StopRotateSlam(){
      rotate = false;
}

    private void CheckEnnnemi(bool isEnnemi, float rightRotate)
    {


        if (gameObjectPointPivot != null)
        {
            gameObjectPointPivot.GetComponent<StateOfEntity>().DestroyProjection(false,Vector3.up);
        }
        if (moveAlone != null)
        {
            GetDirection();
            //Chara.transform.localEulerAngles = Vector3.zero;
            moveAlone.DirProjection = newDir;
            moveAlone.currentPowerOfProjection = forceOfSortie;
        }

    }
     private void CheckEnnnemi(bool isEnnemi, float rightRotate, Vector3 Dir)
    {


        if (gameObjectPointPivot != null)
        {
            gameObjectPointPivot.GetComponent<StateOfEntity>().DestroyProjection(false,Vector3.up);
        }
        if (moveAlone != null)
        {
            GetDirection();
            //Chara.transform.localEulerAngles = Vector3.zero;
            Dir = new Vector3(Dir.x,0,Dir.z);
            moveAlone.DirProjection = Dir;
            moveAlone.currentPowerOfProjection = forceOfSortie;
        }

    }

    private Vector3 GetDirection()
    {
        newDir = (pointPivot - transform.position).normalized;
        if (angleSpeed > 0)
        {
            newDir = Quaternion.Euler(0, -90, 0) * newDir;
        }
        else
        {

            newDir = Quaternion.Euler(0, 90, 0) * newDir;
        }

        return newDir;
    }
    private float GetDistance()
    {
        float newDistance = (pointPivot - transform.position).magnitude;

        return newDistance;
    }

    private Vector3 GetNextDirection()
    {
        Vector3 ecartPointPivot = transform.position - pointPivot;
        Vector3 posIntermediaire = pointPivot + (Quaternion.Euler(0, angleSpeed * predictionMvtRotate, 0) * ecartPointPivot);
        nextDir = (pointPivot - posIntermediaire).normalized;

        if (angleSpeed > 0)
        {
            nextDir = Quaternion.Euler(0, -90, 0) * nextDir;
        }
        else
        {

            nextDir = Quaternion.Euler(0, 90, 0) * nextDir;
        }

        return nextDir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObjectPointPivot == collision.gameObject && tagEnter == null)
        {
            stocks.DetachPlayer();
        }
    }


    //private void OnDrawGizmosSelected()
    //{
    //    if (Camera.current.name == "Camera")
    //    {
    //        if (stocks.ennemiStock != null)
    //        {
    //            Vector3 ecartPointPivot = transform.position - pointPivot;
    //            Vector3 spherePos = pointPivot + (Quaternion.Euler(0, angleSpeed * 0.5f, 0) * ecartPointPivot);
    //            Gizmos.DrawWireSphere(spherePos, 10);
    //        }
    //    }
    //}

    private void OnRenderObject()
    {
        //if (Camera.current.name == "Camera")
        //{
        //    if (stocks.ennemiStock != null)
        //    {
        //        GL.Begin(GL.LINES);
        //        lineMat.SetPass(0);

        //        GL.Color(Color.yellow);
        //        GL.Vertex(transform.position);
        //        GetNextDirection();
        //        GL.Vertex(transform.position + (nextDir).normalized * 100);
        //        GL.End();
        //    }

        //}

    }
}

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

    private GameObject gameObjectPointPivot;
    private Vector3 pointPivot;
    private string tagEnter;

    [HideInInspector] public bool rotate = false;
    private bool changeSens;
    private int i;
    private LineRenderer lineRenderer;
    private LineRend line;
    [HideInInspector] [Range(0, 1)] public float predictionMvtRotate = 0.5f;
    [HideInInspector] public Vector3 newDir;
    [HideInInspector] public Vector3 nextDir;
    private GameObject Chara;
    [HideInInspector] public float angleAvatar = 0;

    private float speedRotationAnim = 70;
    public GameObject vfxShockWave;



    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        forceOfSortie = transform.GetComponent<PlayerMoveAlone>().powerOfProjection;
        line = GetComponentInChildren<LineRend>();
        stocks = GetComponent<EnnemiStock>();
        currentAngleMax = angleMax;
    }

    public void FixedUpdate()
    {

        if (rotate)
        {

            if (tagEnter == tag)
            {
                pointPivot = stocks.ennemiStock.transform.position;
            }
            angleCompteur += Mathf.Abs(angleSpeed) * Time.deltaTime;
            transform.RotateAround(pointPivot, Vector3.up, angleSpeed * Time.deltaTime);


            angleAvatar += speedRotationAnim * Time.deltaTime;   /* Vector3.SignedAngle(Vector3.forward, GetDirection(), Vector3.up);*/
            //Chara.transform.eulerAngles = new Vector3(0, angleAvatar, 0);
            float angle = Vector3.SignedAngle(Vector3.forward, GetDirection(), Vector3.up);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, pointPivot);
            line.p1 = transform.position;
            line.p2 = stocks.ennemiStock.transform.position;
            line.ColliderSize();



            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                ChangeRotationDirection();

            }

            if (angleCompteur > currentAngleMax)
            {
                angleCompteur = 0;
            }

            GetNextDirection();
        }
        else
        {

            angleAvatar = 0;
        }
    }

    public void ChangeRotationDirection()
    {
        angleSpeed = -angleSpeed;

        stocks.inputNeed = !stocks.inputNeed;
        currentAngleMax = angleMax + angleCompteur;
        angleCompteur = 0;
        changeSens = true;
    }

    public bool StartRotation(GameObject objetRotate, GameObject positionPivot, string tag, float forceSortie, bool changeRotate)
    {

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
            }
        }
        if (!changeRotate)
        {
            angleSpeed = Mathf.Abs(angleSpeed);
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
        if (StateAnim.state == StateAnim.CurrentState.Rotate)
        {
            StateAnim.ChangeState(StateAnim.CurrentState.Projection);
        }
        if (isWall)
        {
            transform.tag = tagEnter;
        }

        CheckEnnnemi(isWall, angleSpeed);

        transform.GetComponent<Rigidbody>().AddForce(newDir.normalized * forceOfSortie, ForceMode.Impulse);
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
        rotate = false;
    }

    private void CheckEnnnemi(bool isEnnemi, float rightRotate)
    {


        if (gameObjectPointPivot != null)
        {
            gameObjectPointPivot.GetComponent<Rigidbody>().AddForce(Vector3.up * forceOfSortie, ForceMode.Impulse);
        }
        if (transform.GetComponent<PlayerMoveAlone>())
        {
            GetDirection();
            //Chara.transform.localEulerAngles = Vector3.zero;
            transform.GetComponent<PlayerMoveAlone>().DirProjection = newDir;
            transform.GetComponent<PlayerMoveAlone>().currentPowerOfProjection = forceOfSortie;
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

    //private void OnRenderObject()
    //{
    //    if (Camera.current.name == "Camera")
    //    {
    //        if (stocks.ennemiStock != null)
    //        {
    //            GL.Begin(GL.LINES);
    //            lineMat.SetPass(0);

    //            GL.Color(Color.yellow);
    //            GL.Vertex(transform.position);
    //            GetNextDirection();
    //            GL.Vertex(transform.position + (nextDir).normalized * 100);
    //            GL.End();
    //        }

    //    }

    //}
}

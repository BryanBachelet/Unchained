using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiStock : MonoBehaviour
{
    public GameObject ennemiStock;
    public LineRenderer lineRenderer;
    public bool rotate;
    public bool rotateMe;
    public bool slam;
    public bool slamMe;
    public bool ChangeRotate;
    public bool arriveOnSlam;
    public float angleSpeed = 45;
    public float angleMax = 45;
    public float angleCompteur;
    public float angleCurrentMax;

    EnnemiBehavior myEnnemiStockBhv;
    public Vector3 myPosOnSlam;
    Vector3 ennemyPosOnSlam;
    Vector3 dir;
    Vector3 normal;
    public GameObject slameAOEPrefab;
    // Start is called before the first frame update
    void Start()
    {

        angleCurrentMax = angleMax;
        lineRenderer.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (ennemiStock != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, ennemiStock.transform.position);
            if (rotate && !slam)
            {
                if (Input.GetMouseButtonDown(0) && !ChangeRotate && !slam && !slamMe)
                {

                    angleSpeed = -angleSpeed;
                    angleCurrentMax = angleMax + angleCompteur;
                    angleCompteur = 0;
                    ChangeRotate = true;
                }
                Vector3 previousPos = ennemiStock.transform.position;
                angleCompteur += Mathf.Abs(angleSpeed) * Time.deltaTime;
                ennemiStock.transform.RotateAround(gameObject.transform.position, Vector3.up, angleSpeed * Time.deltaTime);
                if (angleCompteur > angleCurrentMax)
                {
                    Vector3 newDir = ennemiStock.transform.position - previousPos;
                    ennemiStock.tag = "Ennemi";
                    ennemiStock.GetComponent<Rigidbody>().AddForce(newDir.normalized * 60, ForceMode.Impulse);
                    ennemiStock.GetComponent<EnnemiDestroy>().isDestroying = true;
                    ennemiStock = null;
                    rotate = false;
                    ChangeRotate = false;
                    angleCurrentMax = angleMax;
                    angleCompteur = 0;
                }
            }
            else if (rotateMe)
            {
                if (Input.GetMouseButtonDown(1) && !ChangeRotate && !slam && !slamMe)
                {

                    angleSpeed = -angleSpeed;
                    angleCurrentMax = angleMax + angleCompteur;
                    angleCompteur = 0;
                    ChangeRotate = true;
                }
                Vector3 previousPos = transform.position;
                angleCompteur += Mathf.Abs(angleSpeed) * Time.deltaTime;
                transform.RotateAround(ennemiStock.transform.position, Vector3.up, angleSpeed * Time.deltaTime);
                if (angleCompteur > angleCurrentMax)
                {
                    Vector3 newDir = transform.position - previousPos;
                    //ennemiStock.tag = "Ennemi";
                    gameObject.GetComponent<Rigidbody>().AddForce(newDir.normalized * 60, ForceMode.Impulse);
                    ennemiStock.GetComponent<EnnemiDestroy>().isDestroying = true;
                    ennemiStock = null;
                    rotateMe = false;
                    ChangeRotate = false;
                    angleCurrentMax = angleMax;
                    angleCompteur = 0;
                }
            }
            if (Input.GetMouseButtonDown(0) && !slam && !slamMe)
            {
                rotate = true;
            }
            if (Input.GetMouseButtonDown(1) && !slam && !slamMe)
            {
               // angleCompteur = 0;
               // angleCurrentMax = angleMax;
               // ChangeRotate = false;
                rotateMe = true;
            }
            if(slam && !rotate && !rotateMe)
            {
                if(arriveOnSlam)
                {
                    dir = ennemiStock.transform.position - gameObject.transform.position;
                    normal = Vector3.Cross(-dir, Vector3.up);
                    ennemyPosOnSlam = ennemiStock.transform.position;
                    myPosOnSlam = transform.position;
                    arriveOnSlam = false;
                }
                angleCompteur += Mathf.Abs(angleSpeed) * Time.deltaTime;
                if (angleCompteur < 180)
                {
                    ennemiStock.transform.RotateAround(myPosOnSlam, normal, -angleSpeed * Time.deltaTime);                
                }
                else
                {
                    Instantiate(slameAOEPrefab, ennemiStock.transform.position, transform.rotation);
                    ennemiStock.GetComponent<EnnemiDestroy>().isDestroying = true;
                    slam = false;
                    ennemiStock = null;
                    angleCompteur = 0;
                }

            }
            else if (slamMe && !rotate && !rotateMe)
            {
                if (arriveOnSlam)
                {
                    dir = ennemiStock.transform.position - gameObject.transform.position;
                    normal = Vector3.Cross(dir, Vector3.up);
                    ennemyPosOnSlam = ennemiStock.transform.position;
                    myPosOnSlam = transform.position;
                    arriveOnSlam = false;
                }
                angleCompteur += Mathf.Abs(angleSpeed) * Time.deltaTime;
                if (angleCompteur < 180)
                {
                    transform.RotateAround(ennemyPosOnSlam, normal, -angleSpeed * Time.deltaTime);
                }
                else
                {
                    Instantiate(slameAOEPrefab, transform.position, transform.rotation);
                    ennemiStock.GetComponent<EnnemiDestroy>().isDestroying = true;
                    slamMe = false;
                    ennemiStock = null;
                    angleCompteur = 0;
                }

            }
            if (Input.GetKeyDown(KeyCode.A) && !slamMe && !slam)
            {
                arriveOnSlam = true;
                slam = true;
            }
            if (Input.GetKeyDown(KeyCode.E) && !slamMe && !slam)
            {
                arriveOnSlam = true;
                slamMe = true;
            }
        }
        else
        {
            ChangeRotate = false;
            rotate = false;
            rotateMe = false;
            angleSpeed = 120;
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, transform.position);
        }

    }
}

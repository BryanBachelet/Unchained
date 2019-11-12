using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiStock : MonoBehaviour
{
    public GameObject ennemiStock;
    public LineRenderer lineRenderer;
    public bool rotate;
    public bool ChangeRotate;
    public float angleSpeed = 45;
    public float angleMax = 45;
    public float angleCompteur;
    public float angleCurrentMax;

    // Start is called before the first frame update
    void Start()
    {


        lineRenderer.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (ennemiStock != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, ennemiStock.transform.position);
            if (rotate)
            {
                if (Input.GetMouseButtonDown(0) && !ChangeRotate  )
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
                    ChangeRotate = false    ;
                    angleCurrentMax = angleMax;
                    angleCompteur = 0;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                rotate = true;
            }
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }

    }
}

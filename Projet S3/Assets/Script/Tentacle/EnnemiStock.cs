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
    public LayerMask groundLayer;

    public bool onDrop;
    public bool onDropMe;
    private RotationPlayer rotationPlayer;
    private SlamPlayer slamPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rotationPlayer = GetComponent<RotationPlayer>();
        slamPlayer = GetComponent<SlamPlayer>();

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
            if (!slam && !rotate)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    rotate = rotationPlayer.StartRotation(ennemiStock, gameObject, "Ennemi", 60);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", 10);
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    slam = slamPlayer.StartSlam(ennemiStock, gameObject);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    slam = slamPlayer.StartSlam(gameObject, ennemiStock);
                }
            }
        }
        else
        {
            ChangeRotate = false;
            rotate = false;
            rotateMe = false;
            angleSpeed = 120;
        }
    }

    public void StopRotate()
    {
        rotate = false;
        ennemiStock = null;
    }
    public void StopSlam()
    {
        slam = false;
        ennemiStock = null;
    }
}

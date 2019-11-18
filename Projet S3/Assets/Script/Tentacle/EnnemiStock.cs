using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiStock : MonoBehaviour
{
    public GameObject ennemiStock;
    public Klak.Motion.SmoothFollow mySmoothFollow;
    private LineRenderer lineRenderer;
    private bool rotate;
    private bool slam;
    private RotationPlayer rotationPlayer;
    private SlamPlayer slamPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rotationPlayer = GetComponent<RotationPlayer>();
        slamPlayer = GetComponent<SlamPlayer>();
        lineRenderer = GetComponent<LineRenderer>();
   
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
            if(Input.GetKey(KeyCode.Mouse0))
            {
                mySmoothFollow.target = ennemiStock.gameObject.transform;
                rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", 10);
                if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    rotationPlayer.ChangeRotationDirection();
                }
                else if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    rotationPlayer.ChangeRotationDirection();
                }
            }
            else
            {
                ennemiStock.GetComponent<EnnemiBehavior>().imStock = false;
                mySmoothFollow.target = null;
                ennemiStock = null;
            }
            //if (!slam && !rotate)
            //{
            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        rotate = rotationPlayer.StartRotation(ennemiStock, gameObject, "Ennemi", 60);
            //    }
            //    if (Input.GetMouseButtonDown(1))
            //    {
            //        rotate = rotationPlayer.StartRotation(gameObject, ennemiStock, "Player", 10);
            //    }
            //
            //    if (Input.GetKeyDown(KeyCode.A))
            //    {
            //        slam = slamPlayer.StartSlam(ennemiStock, gameObject);
            //    }
            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        slam = slamPlayer.StartSlam(gameObject, ennemiStock);
            //    }
            //}
        }
        else
        {
           
            rotate = false;
            slam = false;
            

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

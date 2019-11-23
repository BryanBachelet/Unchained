using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScope : MonoBehaviour
{
    public Material line;
    public GameObject bullet;
    // public GameObject spawn;
    private EnnemiStock ennemiStock;
    private Vector3 direction;
    private GameObject instanceBullet;
    private LineRenderer lineRenderer;
    private bool returnLine;
    private bool destructBool;
    private Vector3 ballPos;
    private Vector3 returnPos;
    private float distanceReturn;
    private Vector3 dirReturn;
    public float returnSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {

        ennemiStock = GetComponent<EnnemiStock>();
        lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        direction = DirectionSouris();
        if (Input.GetMouseButtonDown(0) /*|| Input.GetMouseButtonDown(2)*/ && ennemiStock.ennemiStock == null && instanceBullet == null)
        {
            instanceBullet = Instantiate(bullet, transform.position + direction.normalized * 0.5f, transform.rotation);
            Projectils projectils = instanceBullet.GetComponent<Projectils>();
            projectils.dir = direction;
            projectils.player = gameObject;
            projectils.moveAlone = GetComponent<PlayerMoveAlone>();

        }
        if (ennemiStock.ennemiStock == null && instanceBullet != null)
        {

            ballPos = instanceBullet.transform.position;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, ballPos);
            returnLine = true;
            destructBool = false;

        }

        if (ennemiStock.ennemiStock == null && instanceBullet == null)
        {
            //Return de line renderer après le tir;
            if (returnLine)
            {
                if (!destructBool)
                {
                    returnPos = ballPos;
                    destructBool = true;
                }
                if (Vector3.Distance(transform.position, returnPos) > 3)
                {

                    float dis = Vector3.Distance(transform.position, returnPos);
                    dirReturn = transform.position - returnPos;


                    if (dis > returnSpeed)
                    {
                    returnPos = returnPos + dirReturn.normalized * dis * Time.deltaTime;

                    }
                    else
                    {
                        returnPos = returnPos + dirReturn.normalized * returnSpeed * Time.deltaTime;
                    }

                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, returnPos); 
                }
                else
                {
                    returnLine = false;
                }
            }
            if (!returnLine)
            {

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position);

            }
        }

    }

    private void OnRenderObject ()
    {


        GL.Begin(GL.LINES);
        line.SetPass(0);
        GL.Color(Color.red);
        GL.Vertex(transform.position);
        GL.Vertex(transform.position + direction.normalized * 100);
        GL.End();

    }
    private void OnDrawGizmos()
    {
        GL.Begin(GL.LINES);
        line.SetPass(0);
        GL.Color(Color.red);
        GL.Vertex(transform.position);
        GL.Vertex(transform.position + direction.normalized * 100);
        GL.End();

    }
    private Vector3 DirectionSouris()
    {
        Ray camera = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rauEnter;

        if (ground.Raycast(camera, out rauEnter))
        {
            Vector3 pointToLook = camera.GetPoint(rauEnter);
            Vector3 posPlayer = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 dir = pointToLook - posPlayer;
          //  Debug.DrawRay(gameObject.transform.position + direction.normalized * 0.5f, dir.normalized * 100, Color.red);

          

            return dir;
        }
        else
        {
            return Vector3.zero;
        }
    }
}

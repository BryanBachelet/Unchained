using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectils : MonoBehaviour
{
    public GameObject player;
    public PlayerMoveAlone moveAlone;
    public Vector3 dir;
    public float speed;
    public float timerOfLife = 5;
    private float compteur;
    public bool returnBall;
    public Vector3 mouvement;
    public LineRenderer lineRenderer;
    public Vector3 hitWallPos;
  
    private void Start()
    {

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.transform.position);


    }

    void Update()
    {
        mouvement = dir.normalized * (speed + moveAlone.currentPowerOfProjection) * Time.deltaTime;
        transform.position += dir.normalized * (speed + moveAlone.currentPowerOfProjection) * Time.deltaTime;
       
    }
    private void LateUpdate()
    {
        lineRenderer.SetPosition(1, player.transform.position);
        lineRenderer.SetPosition(0, transform.position);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (!returnBall)
        {
            if (other.tag == "Ennemi")
            {
                player.GetComponent<EnnemiStock>().ennemiStock = other.gameObject;
                player.GetComponent<EnnemiStock>().onHitEnter = true;
                if (other.GetComponent<EnnemiBehavior>() != null) other.GetComponent<EnnemiBehavior>().useNavMesh = false;

                other.tag = "Untagged";
                other.transform.position += dir.normalized * 3;
                Destroy(gameObject);
            }
            else if (other.tag == "wall")
            {
                //player.GetComponent<EnnemiStock>().ennemiStock = other.gameObject;
                //player.GetComponent<EnnemiStock>().onHitEnter = true;

                //hitWallPos = other.ClosestPoint(transform.position);
                //player.GetComponent<WallRotate>().rotationPoint = hitWallPos;
                //player.GetComponent<EnnemiStock>().pos = hitWallPos;
               
              
                Destroy(gameObject);
            }
        }
       

    }
    private void OnTriggerStay(Collider other)
    {
        if (!returnBall)
        {
            if (other.tag == "Ennemi")
            {
                player.GetComponent<EnnemiStock>().ennemiStock = other.gameObject;
                player.GetComponent<EnnemiStock>().onHitEnter = true;
                if (other.GetComponent<EnnemiBehavior>() != null) other.GetComponent<EnnemiBehavior>().useNavMesh = false;

                other.tag = "Untagged";
                other.transform.position += dir.normalized * 3;
                Destroy(gameObject);
            }
            else if (other.tag == "wall")
            {
                //player.GetComponent<EnnemiStock>().ennemiStock = other.gameObject;
                //player.GetComponent<EnnemiStock>().onHitEnter = true;

                //hitWallPos = other.ClosestPoint(transform.position);
                //player.GetComponent<WallRotate>().rotationPoint = hitWallPos;
                //player.GetComponent<EnnemiStock>().pos = hitWallPos;
               

                //player.GetComponent<WallRotate>().hasHitWall = true;
                Destroy(gameObject);
            }
        }
        
    }


}

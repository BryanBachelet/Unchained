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
    private Vector3 hitPos;
    private float distanceBetweenHitandPlayer;
    private float distanceProjectilePlayer;
    private bool hitWall;
    private void Start()
    {

        if (!lineRenderer.enabled)
        {
           //lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.transform.position);


    }


    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, dir.normalized);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, (speed + 20)) && hit.collider.tag == "wall")
        {
            if (hit.collider.tag == "wall")
            {
                distanceBetweenHitandPlayer = (hit.point - player.transform.position).magnitude;
                hitWall = true;
            }

        }

        Vector3 dirToPlayer = player.transform.position - transform.position;
        Ray rayToPlayer = new Ray(transform.position, dirToPlayer.normalized);
        Debug.DrawRay(transform.position, dirToPlayer.normalized * dirToPlayer.magnitude, Color.blue);
        if (Physics.Raycast(rayToPlayer, out hit, dirToPlayer.magnitude) && hit.collider.tag == "Ennemi")
        {
            if (hit.collider.tag == "Ennemi")
            {
                AttachEntities(hit.collider.gameObject);
            }

        }



        if (hitWall)
        {
            distanceProjectilePlayer = (transform.position - player.transform.position).magnitude;

            if (distanceProjectilePlayer > distanceBetweenHitandPlayer)
            {
                Destroy(gameObject);
            }
        }
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


    public void AttachEntities(GameObject other)
    {
        player.GetComponent<EnnemiStock>().ennemiStock = other.gameObject;
        player.GetComponent<EnnemiStock>().onHitEnter = true;
        if (other.GetComponent<EnnemiBehavior>() != null) other.GetComponent<EnnemiBehavior>().useNavMesh = false;

        other.tag = "Untagged";
        other.transform.position += dir.normalized * 3;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!returnBall)
        {
            if (other.tag == "Ennemi")
            {
                AttachEntities(other.gameObject);
            }
            else if (other.tag == "wall")
            {
                //player.GetComponent<EnnemiStock>().ennemiStock = other.gameObject;
                //player.GetComponent<EnnemiStock>().onHitEnter = true;

                //hitWallPos = other.ClosestPoint(transform.position);
                //player.GetComponent<WallRotate>().rotationPoint = hitWallPos;
                //player.GetComponent<EnnemiStock>().pos = hitWallPos;


                //Destroy(gameObject);
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
                //Destroy(gameObject);
            }
        }

    }


}

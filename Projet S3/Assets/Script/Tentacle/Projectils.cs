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
        mouvement = dir.normalized * (speed + moveAlone.powerProjec) * Time.deltaTime;
        transform.position += dir.normalized * (speed + moveAlone.powerProjec) * Time.deltaTime;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.transform.position);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ennemi")
        {
            player.GetComponent<EnnemiStock>().ennemiStock = other.gameObject;
            player.GetComponent<EnnemiStock>().onHitEnter = true;

            other.tag = "Untagged";
            other.transform.position += dir.normalized * 3;
            Destroy(gameObject);
        }
        else if (other.tag == "wall")
        {
            Debug.Break();
            Destroy(gameObject);
        }

    }
}

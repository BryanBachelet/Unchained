using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendCrist : MonoBehaviour
{
    [HideInInspector]
    public bool active;
    public LineRenderer lineRenderer;
    public BoxCollider box;
    private GameObject p1;
    private GameObject p2;
    private float dot;
    private float distance;
    private EnnemiStock ennemiStock;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.GetComponent<EnnemiStock>())
        {
            ennemiStock = transform.parent.GetComponent<EnnemiStock>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ennemiStock != null)
        {
            if (ennemiStock.ennemiStock != null)
            {
                box.enabled = true;
                active = true;
                p2 = ennemiStock.ennemiStock;
                p1 = transform.parent.gameObject;
            }
            else
            {
                active = false;
                box.enabled = false;
            }
        }

        if (active)
        {
            SetLine();
            ColliderSize();

        }
    }

    void SetLine()
    {
        lineRenderer.SetPosition(0, p2.transform.localPosition);
        lineRenderer.SetPosition(1, p1.transform.localPosition);

    }

    void ColliderSize()
    {
        distance = Vector3.Distance(p1.transform.position, p2.transform.position);
        Vector3 dir = p2.transform.position - p1.transform.position;
        dot = Vector3.SignedAngle(dir.normalized, Vector3.forward, Vector3.up);
        transform.position = p2.transform.position + (-dir.normalized * distance) / 2;
        transform.rotation = Quaternion.Euler(0, -dot, 0);
        box.size = new Vector3(1, 1, distance);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Ennemi")
        {
            collision.transform.tag = "wall";
            Rigidbody ennemyRB = collision.GetComponent<Rigidbody>();
            collision.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            collision.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
            ennemyRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
            collision.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 1);
            //collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
            //collision.GetComponent<EnnemiDestroy>().isDestroying = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESlamCrist : MonoBehaviour
{
    float tempsEcouleVie;
    float tempsVie = 0.5f;
    float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = gameObject.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        tempsEcouleVie += Time.deltaTime;
        if(tempsEcouleVie > tempsVie)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Ennemi")
        {
            collision.transform.tag = "wall";
            collision.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            Rigidbody ennemyRB = collision.GetComponent<Rigidbody>();
            ennemyRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
            collision.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}

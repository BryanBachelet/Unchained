using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESlam : MonoBehaviour
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
            collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
            collision.GetComponent<EnnemiDestroy>().isDestroying = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}

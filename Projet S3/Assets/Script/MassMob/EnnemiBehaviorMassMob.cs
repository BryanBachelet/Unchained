using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiBehaviorMassMob : MonoBehaviour
{
    public Vector3 direction;
   public GameObject target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        direction = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {

        if (gameObject.tag == "bleu")
        {
            if (other.tag == "jaune")
            {
                direction = other.transform.position - transform.position;
            }
            if (other.tag == "rouge")
            {
                direction = other.transform.position - transform.position;
            }
            if (other.tag == "Player")
            {
                direction = other.transform.position - transform.position;
            }
        }

        if (gameObject.tag == "jaune")
        {
            if (other.tag == "bleu")
            {
                direction = other.transform.position - transform.position;
            }
            if (other.tag == "rouge")
            {
                direction = other.transform.position - transform.position;
            }
            if (other.tag == "Player")
            {
                direction = other.transform.position - transform.position;
            }
        }

        if (gameObject.tag == "rouge")
        {
            if (other.tag == "bleu")
            {
                direction = other.transform.position - transform.position;
            }
            if (other.tag == "jaune")
            {
                direction = other.transform.position - transform.position;
            }
            if (other.tag == "Player")
            {
                direction = other.transform.position - transform.position;
            }
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "bleu")
        {
            if (collision.transform.tag == "jaune")
            {
                // collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
                collision.gameObject.GetComponent<EnnemiDestroy>().isDestroying = true;
            }
            if (collision.transform.tag == "rouge")
            {
                // collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
                collision.gameObject.GetComponent<EnnemiDestroy>().isDestroying = true;
            }
        }

        if (gameObject.tag == "rouge")
        {
            Debug.Log("rouge destroy");
            if (collision.transform.tag == "jaune")
            {
                // collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
                collision.gameObject.GetComponent<EnnemiDestroy>().isDestroying = true;
            }
            if (collision.transform.tag == "bleu")
            {
                // collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
                collision.gameObject.GetComponent<EnnemiDestroy>().isDestroying = true;
            }
        }

        if (gameObject.tag == "jaune")
        {
            if (collision.transform.tag == "bleu")
            {
                // collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
                collision.gameObject.GetComponent<EnnemiDestroy>().isDestroying = true;
            }
            if (collision.transform.tag == "rouge")
            {
                // collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
                collision.gameObject.GetComponent<EnnemiDestroy>().isDestroying = true;
            }
        }

    }

}

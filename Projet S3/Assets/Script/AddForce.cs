using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public float radius;
    LayerMask mob;
    // Start is called before the first frame update
    void Start()
    {

        mob = LayerMask.GetMask("Mob");
    }

    // Update is called once per frame
    void Update()
    {
        //Collider[] objectInRange = Physics.OverlapSphere(transform.position, radius, mob);
        //for (int i = 0; i < objectInRange.Length; i++)
        //{
        //    objectInRange[i].GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        //    Debug.Log("TTTTTTTTTTOUUUUUUUUUUUUUUUUUUUCCCCCCCCHHHHHHHHHEEEEE");
        //}
    }

    public void Expluse(GameObject mobToExpluse)
    {
        //Debug.Log("DEGAAAAAAAAAAAGE");
        Collider[] objectInRange = Physics.OverlapSphere(transform.position, radius, mob);
        for(int i = 0; i < objectInRange.Length; i++)
        {
            if(objectInRange[i].gameObject == mobToExpluse)
            {
                Debug.Log("DEGAAAAAAAAAAAGE");
                if (Vector3.Dot(Vector3.forward, mobToExpluse.transform.position - transform.position) < 0)
                {
                    objectInRange[i].GetComponent<Rigidbody>().AddForce(-Vector3.up * 10, ForceMode.Impulse);
                    Debug.Log("DEGAAAAAAAAAAAGE");
                }
                else
                {
                    objectInRange[i].GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
                    Debug.Log("DEGAAAAAAAAAAAGE");
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

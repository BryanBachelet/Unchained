using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFall : MonoBehaviour
{
   
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Ennemi")
        {
            // collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
            Destroy(collision.gameObject);
            Debug.Log("DESTROYYYY");
        }
    }


}


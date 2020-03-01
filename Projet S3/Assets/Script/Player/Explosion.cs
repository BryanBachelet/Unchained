using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int range;
    public GameObject feedBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            ExplosionTransformation();
            Debug.Log("Call explod");
        }
    }

    public void ExplosionTransformation()
    {
        Instantiate(feedBack, transform.position,transform.rotation);
        Collider[] entityInRange = Physics.OverlapSphere(transform.position, range);
        for(int i = 0; i < entityInRange.Length; i++)
        {
            if(entityInRange[i].tag == "Ennemi")
            {
                entityInRange[i].GetComponent<EnnemiDestroy>().isDestroying = true;
                entityInRange[i].GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
                Debug.Log("degage");
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

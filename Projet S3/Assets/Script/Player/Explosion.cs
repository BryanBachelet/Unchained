using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int range;
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
        Collider[] entityInRange = Physics.OverlapSphere(transform.position, range);
        for(int i = 0; i < entityInRange.Length; i++)
        {
            if(entityInRange[i].tag == "Ennemi")
            {
                entityInRange[i].GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
                Debug.Log("degage");
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

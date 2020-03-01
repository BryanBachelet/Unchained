using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int range;
    public GameObject feedBack;

    private PlayerMoveAlone moveAlone;
    // Start is called before the first frame update
    void Start()
    {
        moveAlone = GetComponent<PlayerMoveAlone>();
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
                EnnemiDestroy ennemi = entityInRange[i].GetComponent<EnnemiDestroy>();
                ennemi.isDestroying = true;
                Vector3 dir = entityInRange[i].transform.position - transform.position;
                ennemi.dirHorizontalProjection = dir;
                ennemi.currentForceOfEjection = moveAlone.expulsionStrengh;
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

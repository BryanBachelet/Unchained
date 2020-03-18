using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraRepulsion : MonoBehaviour
{
   public PlayerMoveAlone moveAlone;

    // Start is called before the first frame update
    void Start()
    {
        moveAlone = GetComponentInParent<PlayerMoveAlone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ennemi" && other != null && moveAlone != null)
        {
            Vector3 dir = transform.parent.position - other.transform.position;
            other.GetComponent<StateOfEntity>().DestroyProjection(true,dir.normalized);
            //moveAlone.Repulsion(other.gameObject, transform);
        }
    }
}

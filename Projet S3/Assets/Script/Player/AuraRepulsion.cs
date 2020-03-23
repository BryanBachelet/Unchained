using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraRepulsion : MonoBehaviour
{
   public PlayerMoveAlone moveAlone;
private KillCountPlayer  countPlayer;
    // Start is called before the first frame update
    void Start()
    {
             countPlayer = transform.parent.GetComponentInChildren<KillCountPlayer>();
        moveAlone = GetComponentInParent<PlayerMoveAlone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ennemi" && other != null && moveAlone != null)
        {
            Vector3 dir = transform.parent.position - other.transform.position;
            other.GetComponent<StateOfEntity>().DestroyProjection(true,dir.normalized);
            countPlayer.HitEnnemi();
            //moveAlone.Repulsion(other.gameObject, transform);
        }
    }
}

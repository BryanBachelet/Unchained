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
        if(other.tag == "Ennemi" && other != null)
        {
            Debug.Log(moveAlone);
            moveAlone.Repulsion(other.gameObject, transform);
        }
    }
}

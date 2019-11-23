using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities_touche : MonoBehaviour
{
    Animator anim;
  

        private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        
            if (other.gameObject.tag == "Bullet")
            {
                anim.SetBool("Attrapé", true);

        }

        
    }

}

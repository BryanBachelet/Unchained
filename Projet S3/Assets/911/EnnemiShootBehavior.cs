using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiShootBehavior : MonoBehaviour
{
   
    public float radius;
    public GameObject Target;
    public GameObject shot;
    public float shootTime;
    private float compteur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] player = Physics.OverlapSphere(transform.position, radius);
        float dist = 1000;
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].tag == "Player")
            {
                float j = Vector3.Distance(transform.position, player[i].transform.position);
                if (j < dist)
                {
                    Target = player[i].gameObject;
                }
            }
        }
        if (Target != null & compteur>shootTime)
        {
            Shoot();
            compteur = 0;
        }
        compteur += Time.deltaTime;
    }

    private void Shoot()
    {
      GameObject ball =   Instantiate(shot, transform.position, transform.rotation);
        ball.GetComponent<BallBehavior>().dir = (Target.transform.position - transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

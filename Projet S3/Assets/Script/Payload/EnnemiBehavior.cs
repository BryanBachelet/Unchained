using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnnemiBehavior : MonoBehaviour
{
    public bool useNavMesh;
    private NavMeshAgent agent;

    public GameObject target;
    public GameObject currentTarget;
    public float speed;
    public bool isOnSlam;

    private bool faction;
    GameObject player;
    public bool imStock;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
          currentTarget = target;
        if (useNavMesh)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.tag != "wall")
        {
            currentTarget = target;
        }
        if (!useNavMesh)
        {
            if(imStock)
            {
                Vector3 direction = (transform.position - player.transform.position).normalized;
                transform.Translate(direction * 10f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
                tag = "Ennemi";
            }

        }
        if (useNavMesh)
        {
            agent.SetDestination(currentTarget.transform.position);
        }
    }


}


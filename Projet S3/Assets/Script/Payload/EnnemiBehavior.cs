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
    // Start is called before the first frame update
    void Start()
    {
          currentTarget = target;
        if (useNavMesh)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = target;
        }
        if (!isOnSlam && !useNavMesh)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        }
        if (!isOnSlam && useNavMesh)
        {
            agent.SetDestination(currentTarget.transform.position);
        }
    }


}


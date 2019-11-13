using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiBehavior : MonoBehaviour
{

    public GameObject target;
    public GameObject currentTarget;
    public float speed;
    public bool isOnSlam;
    private AgentFaction agentFaction;
    private bool faction;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<AgentFaction>())
        {
            agentFaction = GetComponent<AgentFaction>();
        }
        else
        {
            faction = false;
        }
        currentTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget == null)
        {
            currentTarget = target; 
        }
        if (!isOnSlam)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (faction)
        {
            if (collision.gameObject.GetComponent<AgentFaction>() && collision.gameObject.GetComponent<AgentFaction>().factions != agentFaction.factions)
            {
                collision.gameObject.GetComponent<EnnemiDestroy>().isDestroying = true;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFactions : MonoBehaviour
{
    private AgentFaction agentFaction;
    private EnnemiBehavior ennemiBehavior;

    private void Start()
    {
        ennemiBehavior = GetComponentInParent<EnnemiBehavior>();
        agentFaction = GetComponentInParent<AgentFaction>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AgentFaction>() && other.GetComponent<AgentFaction>().factions != agentFaction.factions)
        {
            ennemiBehavior.currentTarget = other.gameObject;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterAgent : MonoBehaviour
{
    public Transform[] agentList = new Transform[0];
    private Vector3[] posSphere;
    public int circle;
    public float circleLayer;
    public float distance = 1.5f;
    public int multipler = 2;
    private bool active;
    private bool stop;
    public float timing;
    public float timeExplosion = 2;
    private float compteurExplosion;
    private Light light;
    private Camera cam;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        DetectAgent();
        RandomSphere();
        light = GetComponentInChildren<Light>();
        cam = Camera.main;
        pos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) MoveSphere();
        if (Input.GetKey(KeyCode.X))
        {
            active =true;
        }

        if (Vector3.Distance(agentList[0].position, posSphere[0]) < 1)
        {

            
                active = true;
        }
        if (active &&!stop)
        {
            if (compteurExplosion > timeExplosion)
            {
                ExploseAgent();
                cam.transform.position = pos;
                stop = true;
            }
            else
            {
                cam.transform.position = pos + (cam.transform.right * (Random.Range(-3f, 3f) *(compteurExplosion/timeExplosion))) + (cam.transform.forward * (Random.Range(-3f, 3f) * (compteurExplosion / timeExplosion))) + (cam.transform.up * (Random.Range(-3f, 3f) * (compteurExplosion / timeExplosion)));
                compteurExplosion += Time.deltaTime;
                light.intensity = Mathf.Lerp(0, 5, compteurExplosion / timeExplosion);
                light.range = Mathf.Lerp(0, 50, compteurExplosion / timeExplosion);
            }
            

        }
    }

    public void ExploseAgent()
    {
        for (int i = 0; i <agentList.Length; i++)
        {
            Vector3 dir = agentList[i].position - transform.position;
            Rigidbody agent = agentList[i].GetComponent<Rigidbody>();
            agent.AddForce(dir.normalized * 30, ForceMode.Impulse);
        }
    }

    public void DetectAgent()
    {
        LayerMask layer = ~1 << 8;
        Collider[] agent = Physics.OverlapSphere(transform.position, 300, layer);
        agentList = new Transform[agent.Length];
        posSphere = new Vector3[agent.Length];
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].tag == "Ennemi")
            {
                agentList[i] = agent[i].transform;
            }
        }
    }

    public void ActiveAgent()
    {
        int entier = 0;
        int circleDefault = circle;
        float distanceDefault = distance;
        for (int j = 0; j < circleLayer; j++)
        {
            float angle = j != 0 ? 360 / (circle * j*2) : 360 / circle;
            int k = 0;
            int heigh = 0;
            for (int i = entier; i < ((circle + circle * j*2) *2); i++)
            {
                if(i>(circle+circle * j * 2) + (circle + circle * j * 2)/2)
                {
                    heigh = 8;
                }
                Debug.Log(heigh);
                Vector3 destination = (Quaternion.Euler(0, angle * k, 0) * (transform.forward * distanceDefault + transform.up*heigh)) + transform.position;
                agentList[i].position = Vector3.Lerp(agentList[i].position, destination, timing);
                k++;
                entier++;
            }

            distanceDefault++;
            circleDefault += circle;
        }
        timing += Time.deltaTime/10;
    }

    void RandomSphere()
    {
        for (int i = 0; i < agentList.Length; i++)
        {
            posSphere[i] = transform.position + Random.insideUnitSphere * 1.5f;
            agentList[i].position = transform.position + ( new Vector3(Random.insideUnitCircle.x,0, Random.insideUnitCircle.y).normalized *30);
        }
    }

    void MoveSphere()
    {
        for (int i = 0; i < agentList.Length; i++)
        {
            if (Vector3.Distance(agentList[i].position, posSphere[i])<10){

            agentList[i].position = Vector3.Lerp(agentList[i].position, posSphere[i], timing);
            }
            else
            {
                agentList[i].position = Vector3.Lerp(agentList[i].position,( posSphere[i] - new Vector3 (0, posSphere[i].y,0)), timing);
            }

            
        }
        timing += Time.deltaTime/20;
    }
}

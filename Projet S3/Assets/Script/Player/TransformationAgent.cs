using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationAgent : MonoBehaviour
{
    public List<Transform> agentList = new List<Transform>();
    private Vector3[] posSphere;

    public float distance = 1.5f;

    public float speedOfAgent;

    public float lightIntensity = 2;
    public float lightRange = 20;

    public float distanceGrap = 50;

    private bool active;
    private bool stop;
    private float timing;
    private float timeExplosion = 2;
    private float compteurExplosion;
    private Light lightPlayer;

    private int frame;
    private bool startAnim;
    private Vector3 pos;

    public GameObject explosionVFX;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (this.enabled == true)
        {
            lightPlayer = GetComponentInChildren<Light>();
            frame = 0;
            agentList.Clear();
            startAnim = false;
            active = false;

        }
    }


    void Update()
    {
        if (startAnim)
        {
            if (!stop)
            {
                MoveSphere();
            }

            if (!active)
            {
                if (Vector3.Distance(agentList[0].position, posSphere[0]) < 1)
                {

                    active = true;

                }
            }
            else
            {



                lightPlayer.intensity = Mathf.Lerp(0, lightIntensity, compteurExplosion / timeExplosion);
                lightPlayer.range = Mathf.Lerp(0, lightRange, compteurExplosion / timeExplosion);
                compteurExplosion += Time.deltaTime;



            }


        }


    }

    public void startTranformationAnim(float timeExplosionGive)
    {
        this.timeExplosion = timeExplosionGive;
        DetectAgent();
        RandomSphere();
        startAnim = true;
        stop = false;
    }


    public void ActiveExplosion()
    {

        stop = true;

        ExploseAgent();

    }


    public void ExploseAgent()
    {

        Instantiate(explosionVFX, transform.position, Quaternion.Euler(-90,0,0));
        for (int i = 0; i < agentList.Count; i++)
        {

            agentList[i].GetComponent<EnnemiDestroy>().ActiveExplosion();


            Vector3 dir = agentList[i].position - transform.position;
            Rigidbody agent = agentList[i].GetComponent<Rigidbody>();
            agent.AddForce(dir.normalized * 70, ForceMode.Impulse);
        }

    }

    public void DetectAgent()
    {
        LayerMask layer = ~1 << 8;
        Collider[] agent = Physics.OverlapSphere(transform.position, 50, layer);
        ;
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].tag == "Ennemi")
            {
                agentList.Add(agent[i].transform);


            }
        }
        posSphere = new Vector3[agentList.Count];
    }


    void RandomSphere()
    {
        for (int i = 0; i < agentList.Count; i++)
        {
            posSphere[i] = new Vector3(transform.position.x, 2, transform.position.z) + Random.insideUnitSphere * distance;

        }
    }

    void MoveSphere()
    {
        for (int i = 0; i < agentList.Count; i++)
        {

            if (Vector3.Distance(agentList[i].position, posSphere[i]) < 10)
            {

                agentList[i].position = Vector3.Lerp(agentList[i].position, posSphere[i], timing);
            }
            else
            {
                agentList[i].position = Vector3.Lerp(agentList[i].position, (posSphere[i] - new Vector3(0, posSphere[i].y - 1, 0)), timing);
            }

            agentList[i].eulerAngles = Vector3.zero;

            if (frame > 1 && frame < 3)
            {
                agentList[i].GetComponent<Rigidbody>().useGravity = false;

            }
        }
        timing = speedOfAgent * Time.deltaTime;
        if (frame > 0 && frame < 3)
        {
            transform.GetComponent<PlayerMoveAlone>().enabled = false;
            Physics.IgnoreLayerCollision(9, 9, true);
            Physics.IgnoreLayerCollision(9, 10, true);

        }
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        frame++;
    }
}

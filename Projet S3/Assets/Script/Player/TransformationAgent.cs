using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationAgent : MonoBehaviour
{
    public List<Transform> agentList = new List<Transform>();
    private Vector3[] posSphere;

    public float distance = 1.5f;

    private bool active;
    private bool stop;
    public float timing;
    public float timeExplosion = 2;
    private float compteurExplosion;
    private Light lightPlayer;

    private int frame;
    private bool startAnim;
    private Vector3 pos;
    // Start is called before the first frame update
    void OnEnable()
    {
        DetectAgent();
        RandomSphere();
        lightPlayer = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startAnim)
        {
            if (!stop)
            {
                MoveSphere();
            }
            if (Input.GetKey(KeyCode.X))
            {
                active = true;
            }

            if (Vector3.Distance(agentList[0].position, posSphere[0]) < 1)
            {

                active = true;

            }
            if (active)
            {



                lightPlayer.intensity = Mathf.Lerp(0, 5, compteurExplosion / timeExplosion);
                lightPlayer.range = Mathf.Lerp(0, 50, compteurExplosion / timeExplosion);
                compteurExplosion += Time.deltaTime;



            }

        }


    }

    public void startTranformationAnim(float timeExplosionGive)
    {
        this.timeExplosion = timeExplosionGive;
        startAnim = true;
    }


    public void ActiveExplosion()
    {

        stop = true;
        ExploseAgent();

    }


    public void ExploseAgent()
    {
        for (int i = 0; i < agentList.Count; i++)
        {
           
          //  agentList[i].GetComponent<EnnemiDestroy>().isDestroying = true;
            Vector3 dir = agentList[i].position - transform.position;
            Rigidbody agent = agentList[i].GetComponent<Rigidbody>();
            agent.AddForce(dir.normalized * 50, ForceMode.Impulse);
        }
        transform.GetComponent<PlayerMoveAlone>().enabled = true;
    }

    public void DetectAgent()
    {
        LayerMask layer = ~1 << 8;
        Collider[] agent = Physics.OverlapSphere(transform.position, 300, layer);
        ;
        for (int i = 0; i < agent.Length; i++)
        {
            if (agent[i].tag == "Ennemi")
            {
                agentList.Add(agent[i].transform);
                agent[i].GetComponent<EnnemiBehavior>().enabled = false;

              
                Debug.Log(Physics.GetIgnoreLayerCollision(9, 9));



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
     
            if (frame > 1)
            {
                agentList[i].GetComponent<Rigidbody>().useGravity = false;
            }
        }
        timing = Time.deltaTime /*/ 20*/;
        if (frame > 1)
        {
            transform.GetComponent<PlayerMoveAlone>().enabled = false;
            Physics.IgnoreLayerCollision(9, 9);
            Physics.IgnoreLayerCollision(9, 10);
        }
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        frame++;
    }
}

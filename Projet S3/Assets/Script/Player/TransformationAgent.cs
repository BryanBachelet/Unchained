using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationAgent : MonoBehaviour
{
    public List<Transform> agentList = new List<Transform>();
    private Vector3[] posSphere;
    private Vector3[] posStart;
    private Quaternion[] angleSphere;
    public int distance = 1;

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
   // public float numberMax = 50;
    public int Height;
    public int numberMax;
    public float radius;

    public float timeToCome;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (this.enabled == true)
        {
            distanceGrap = 10;
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
        RandomSphere(true);
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
            
                agentList[i].tag ="Untagged";
            Vector3 dir = agentList[i].position - transform.position;
            agentList[i].GetComponent<StateOfEntity>().DestroyProjection(false,dir); 
        
            LineRenderer line =  agentList[i].GetComponent<LineRenderer>();
            line.enabled = false;
           

      
        }
        StateOfGames.currentPhase++;
    }

    public void DetectAgent()
    {
        LayerMask layer = ~1 << 8;
        for(int j = 0 ;j< 10;j++)
        {
        Collider[] agent = Physics.OverlapSphere(transform.position, distanceGrap, layer);
            if(agentList.Count < numberMax )
            {
                for (int i = 0; i < agent.Length; i++)
                {
                    
                    
                    if (agent[i].tag == "Ennemi" && agentList.Count<=numberMax  )
                    {
                        if(agent[i].GetComponent<StateOfEntity>().entity == StateOfEntity.EntityState.Formation || agent[i].GetComponent<StateOfEntity>().entity == StateOfEntity.EntityState.ReturnFormation )
                        {

                            if(!agentList.Contains(agent[i].transform))
                            {
                            agentList.Add(agent[i].transform);
                            agent[i].GetComponent<StateOfEntity>().entity = StateOfEntity.EntityState.Catch;
                            }
                        }
                    


                    }
                }
            }
            distanceGrap +=10;
        }
        posSphere = new Vector3[agentList.Count];
        posStart = new Vector3[agentList.Count];
        angleSphere = new Quaternion[agentList.Count];
    }


    void RandomSphere(bool circle)
    {
            int k =0; 
        if(!circle)
        {
            for (int j = 0; j < this.Height; j++) 
            {
                
                    
                var circlePerimeter = (2f * Mathf.PI * this.distance-j) + 1;
                for (int i = 0; i < circlePerimeter; i++) 
                {
                    float angle = 360f / circlePerimeter * i;
                    Vector3 position = transform.position + new Vector3(0,j,0) + Quaternion.Euler(0, angle, 0) * new Vector3(this.distance-j, 0, 0);
                    Quaternion dir = Quaternion.LookRotation(position - (this.transform.position + new Vector3(0,0,0)));
            
                    posSphere[k] = position;
            
                    angleSphere[k] =  dir;
                k++;
                k = Mathf.Clamp(k,0,numberMax-1);
                }
            }
        }
        float anglePerAgent = 360 /(agentList.Count+1);
        for(int i =0; i<agentList.Count;i++ )
        {   
          posSphere[i] = transform.position +(Quaternion.Euler(0,anglePerAgent*(i+1),0)  * -Vector3.forward * radius);
          posStart[i] = agentList[i].position;
        }
        
    }

    void MoveSphere()
    {
        for (int i = 0; i < agentList.Count; i++)
        {

            
            agentList[i].position = Vector3.Lerp(posStart[i], posSphere[i], timing);
           

            agentList[i].rotation = angleSphere[i];

      
            LineRenderer line =  agentList[i].GetComponent<LineRenderer>();
            line.enabled =true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1,agentList[i].transform.position );

        

            if (frame > 1 && frame < 3)
            {
                agentList[i].GetComponent<Rigidbody>().useGravity = false;

            }
        }
        timing += (1/timeToCome)* Time.deltaTime;
        if (frame > 0 && frame < 3)
        {
            transform.GetComponent<PlayerMoveAlone>().enabled = false;
            //Physics.IgnoreLayerCollision(9, 9, true);
            Physics.IgnoreLayerCollision(9, 10, true);

        }
      
        frame++;
    }
}

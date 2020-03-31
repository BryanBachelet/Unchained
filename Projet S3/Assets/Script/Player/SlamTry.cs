using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamTry : MonoBehaviour
{
    public GameObject agent;
    public Transform point1;
    public Transform point2;
    public float time1;
    public float pausetime;
    public float time2;
    public LayerMask layer;
    public float jumpPlayer = 2;
    public float timerJump;
    public float forceProjection;
    public float distanceForward = 10;
    private Vector3 posAgent;
    private Vector3 posPlayer;

    private float t;
    private float t1;
    private float compteur;

    private enum ProjectioState { Start, Jump, SlamPhase1, SlamPhase2, Projection, Finish }
    private ProjectioState currentState = 0;
    private LineRenderer lineRenderer;
    private Rigidbody rigid;
    private EnnemiStock ennemiStock;
    private KillCountPlayer countPlayer;
    private PlayerMoveAlone playerMove;
    private Vector3 dir;
    public GameObject feed;
    // Start is called before the first frame update
    void Start()
    {
        countPlayer = GetComponentInChildren<KillCountPlayer>();
        ennemiStock = GetComponent<EnnemiStock>();
        lineRenderer = GetComponent<LineRenderer>();
        playerMove = GetComponent<PlayerMoveAlone>();
        rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        

        if (agent != null)
        {
            if (currentState == ProjectioState.Projection || currentState == ProjectioState.Finish)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position);
            }
            else
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, agent.transform.position);
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManagement.LoadCurrentScene();
        }

    /*    if (Input.GetKey(KeyCode.A) && currentState == ProjectioState.Start)
        {
            StartSlam(agent);

        }*/
        switch (currentState)
        {
            case ProjectioState.Jump:
                JumpSlam();
                break;
            case ProjectioState.SlamPhase1:
                PhaseOne();
                break;
            case ProjectioState.SlamPhase2:

                PhaseTwo();
                break;
            case ProjectioState.Projection:
               ennemiStock.DetachPlayer(dir);
                currentState = ProjectioState.Finish;
               // Projection();
                break;
            case ProjectioState.Finish:
                agent = null;
                compteur = 0;
                t = 0;
                point2.transform.position = transform.position;
                point1.transform.position = transform.position;
                feed.SetActive(false);
                break;
        }

        point2.transform.position = transform.position + dir.normalized * distanceForward;
        point2.transform.position = new Vector3( point2.transform.position.x,0, point2.transform.position.z);

    }

    public void StartSlam(GameObject agentGive)
    {
        agent = agentGive;
        posAgent = agent.transform.position;
        dir = transform.position - agent.transform.position;
        posPlayer = transform.position;
        point1.transform.position += Vector3.up * jumpPlayer;
        currentState = ProjectioState.Jump;
        playerMove.DeactiveStickHGround();

    }
    private void JumpSlam()
    {

        rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
        currentState = ProjectioState.SlamPhase1;


    }
    private void PhaseOne()
    {

        agent.transform.position = Vector3.Lerp(posAgent, point1.transform.position, t);
        t = compteur / time1;
        compteur += Time.deltaTime;
        if (compteur > time1 + pausetime)
        {
            t = 0;

            compteur = 0;
            currentState = ProjectioState.SlamPhase2;

        }
    }

    private void PhaseTwo()
    {
        feed.SetActive(true);
        feed.transform.position = point2.transform.position;
        feed.transform.localScale = Vector3.one *25;
        agent.transform.position = Vector3.Lerp(point1.transform.position, point2.transform.position, t);
        t = compteur / time2;
        compteur += Time.deltaTime;
        if (t > 1)
        {
            Collider[] ennmi = Physics.OverlapSphere(point2.transform.position, 25, layer);

            for (int i = 0; i < ennmi.Length; i++)
            {
                Vector3 dir = ennmi[i].transform.position - point2.transform.position;
              //  ennmi[i].GetComponent<Rigidbody>().AddForce(dir * forceProjection, ForceMode.Impulse); 
                ennmi[i].GetComponent<StateOfEntity>().DestroyProjection(false,dir) ;
               // countPlayer.HitEnnemi();
            }

            compteur = 0;
            t = 0;
            currentState = ProjectioState.Projection;
        }

    }
    private void Projection()
    {
        rigid.AddForce(-Vector3.right * 100, ForceMode.Impulse);
        rigid.AddForce(-Vector3.up * 20, ForceMode.Impulse);
        currentState = ProjectioState.Finish;
    }
}


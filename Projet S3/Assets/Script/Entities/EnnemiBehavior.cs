using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehavior : MonoBehaviour
{
    [HideInInspector] public bool useNavMesh;
    private NavMeshAgent agent;

    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject currentTarget;
    public float speedClassic = 4;
    public float speedLinks = 10;
    public float poids = 0;
    GameObject player;
    private float t = 0;
    public float speedOfRotation = 1;
    private float angle;
    [HideInInspector] public bool imStock;
    private int i;
    private Rigidbody rigidbodyEntities;
    private EnnemiDestroy destroy;
    private bool isUse;

    public bool beenKicked = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyEntities = GetComponent<Rigidbody>();
        player = PlayerMoveAlone.Player1;
        currentTarget = target;
        if (useNavMesh)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        destroy = GetComponent<EnnemiDestroy>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
       
        if(beenKicked)
        {
            rigidbodyEntities.AddRelativeForce(Vector3.left * 0.05f, ForceMode.Impulse);
        }
        if (currentTarget == null)
        {
            currentTarget = target;
        }
        if (imStock)
        {
            destroy.isDestroying = false;
        }
        if (!useNavMesh && destroy.isDestroying == false)
        {

            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            rigidbodyEntities.velocity = Vector3.zero;
            if (imStock)
            {
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                Vector3 direction = (transform.position - player.transform.position).normalized;
                transform.Translate(direction * speedLinks * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                transform.eulerAngles = new Vector3(0, 0, 0);
                Debug.DrawRay(transform.position, direction * 100, Color.blue);
            }
            else
            {
                if (Vector3.Distance(transform.position, currentTarget.transform.position) > 1.5f)
                {
                    if (i > 10)
                    {
                        i = 0;
                        Vector3 rot = transform.eulerAngles;
                        if (rot.y > 180)
                        {
                            rot = new Vector3(0, (rot.y - 180) - 180, 0);
                        }

                        Vector3 dir = currentTarget.transform.position - transform.position;
                        dir = new Vector3(dir.x, 0, dir.z);
                        angle = Vector3.SignedAngle(transform.forward, dir.normalized, Vector3.up);

                        t += speedOfRotation * Time.deltaTime;

                        if (t >= 1)
                        {
                            t = 0;
                        }
                        transform.eulerAngles = Vector3.Lerp(rot, rot + new Vector3(0, angle, 0), t);
                    }
                    else
                    {
                        i++;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speedClassic * Time.deltaTime);
                    tag = "Ennemi";
                    //Test///////////////////////////////////////////////////// 
                    //if (rigidbody.velocity.magnitude > 0)
                    //{
                    //    rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, Time.deltaTime);

                    //}
                    //////////////////////////////////////////////////
                }
            }

        }
        if (useNavMesh)
        {
            agent.SetDestination(currentTarget.transform.position);
        }
    }



    //private void OnBecameVisible()
    //{
    //    enabled = true;
    //}
    //private void OnBecameInvisible()
    //{
    //    Debug.Log("1");
    //    enabled = false;
    //    if (Vector3.Distance(transform.position, PlayerMoveAlone.playerPos) > 100)
    //    {
    //    }
    //}

}
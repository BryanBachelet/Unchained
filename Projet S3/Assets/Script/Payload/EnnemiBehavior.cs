﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemiBehavior : MonoBehaviour
{
    public bool useNavMesh;
    private NavMeshAgent agent;

    public GameObject target;
    public GameObject currentTarget;
    public float speedClassic = 4;
    public float speedLinks = 10;
    GameObject player;
    private float t = 0;
    public float speedOfRotation = 1;
    public float angle;
    [HideInInspector] public bool imStock;
    private int i;
    private Rigidbody rigidbody;
    private EnnemiDestroy destroy;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        if (currentTarget == null)
        {
            currentTarget = target;
        }
        if (!useNavMesh && destroy.isDestroying == false)   
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);

            if (imStock)
            {
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y,0) ;
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
                    if (i > 20)
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
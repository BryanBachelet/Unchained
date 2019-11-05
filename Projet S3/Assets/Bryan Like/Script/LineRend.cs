﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRend : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public LineRenderer lineRenderer;
    public BoxCollider box;
    public float dot;
    public float distance;
    public PlayerState playerState;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetLine();
        ColliderSize();
        
    }

    void SetLine()
    {
        lineRenderer.SetPosition(0, p2.transform.localPosition);
        lineRenderer.SetPosition(1, p1.transform.localPosition);

    }

    void ColliderSize()
    {
        distance = PlayerCommand.DistanceBetweenPlayer();
        Vector3 dir = PlayerCommand.DirectionBetweenPlayer();
        dot = Vector3.SignedAngle(dir.normalized, Vector3.forward, Vector3.up);
        transform.position = p2.transform.position + (PlayerCommand.DirectionBetweenPlayer()) / 2;
        transform.rotation = Quaternion.Euler(0, -dot, 0);
        box.size = new Vector3(1, 1, distance);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Ennemi" && playerState.currentState == PlayerState.PlayerCurrentState.Vol)
        {
            collision.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
            //  Destroy(collision.gameObject);
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectils : MonoBehaviour
{
    public GameObject player;
    public PlayerMoveAlone moveAlone;
    public Vector3 dir;
    public float speed;
    public float timerOfLife = 5;
    private float compteur;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (compteur > timerOfLife)
        {
            Destroy(gameObject);
        }
        else
        {
            compteur += Time.deltaTime;
        }

        transform.position += dir.normalized * (speed + moveAlone.powerProjec) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ennemi")
        {
            player.GetComponent<EnnemiStock>().ennemiStock = other.gameObject;
            player.GetComponent<EnnemiStock>().onHitEnter = true;

            other.tag = "Untagged";
            other.transform.position += dir.normalized * 3;
            Destroy(gameObject);
        }
        else if (other.tag == "wall")
        {
            Destroy(gameObject);
        }

    }
}

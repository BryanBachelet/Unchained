﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    public GameObject player;
    private MouseScope playerMouseScope;
    private EnnemiStock playerEnnemiStock;
    public float startDistanceBulletDezoom;
    private Vector3 ecartJoueur;
    private Vector3 basePosition;
    private float distanceBullet;
    private float distanceAgent;
    private float compteurDezoomBullet;
    private float compteurZoomBullet;
    private float competeur;
    public float speedDezoomBullet;
    public float speedDezoomEnnemi;
    public float speedZoomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ecartJoueur = transform.position - player.transform.position;
        playerMouseScope = player.GetComponent<MouseScope>();
        playerEnnemiStock = player.GetComponent<EnnemiStock>();
    }


    void Update()
    {
        basePosition = player.transform.position + ecartJoueur;
        
        if (playerMouseScope.instanceBullet != null || playerEnnemiStock.ennemiStock != null)
        {
            if (playerMouseScope.instanceBullet != null)
            {
               
                distanceBullet = Vector3.Distance(player.transform.position, playerMouseScope.instanceBullet.transform.position);
                if (distanceBullet > 10)
                {
                    compteurDezoomBullet += Time.deltaTime /speedDezoomBullet;
                    compteurZoomBullet = 0;
                    Vector3 camPos = basePosition + -transform.forward * ((distanceBullet - 10) / 1.5f);
                    transform.position = Vector3.Lerp(transform.position, camPos, compteurDezoomBullet);
                }
                else
                {
                    compteurZoomBullet += Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, basePosition, compteurZoomBullet);
                }
            }
            if (playerEnnemiStock.ennemiStock != null)
            {
                distanceAgent = Vector3.Distance(player.transform.position, playerEnnemiStock.ennemiStock.transform.position);
                
                Vector3 dir = playerEnnemiStock.ennemiStock.transform.position -  player.transform.position;
                if (distanceAgent > startDistanceBulletDezoom)
                {
                
                    compteurDezoomBullet += Time.deltaTime/speedDezoomEnnemi;
                    compteurZoomBullet = 0;
                    Vector3 camPos = basePosition + -transform.forward * (distanceAgent-startDistanceBulletDezoom);
                    transform.position = Vector3.Lerp(transform.position, camPos, compteurDezoomBullet);
                }
                else
                {
                    compteurZoomBullet += Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, basePosition, compteurZoomBullet);
                    compteurDezoomBullet = 0;
                }
                
                dir = new Vector3(dir.x, 0, dir.z);
                float dot = Vector3.Dot(dir.normalized, player.transform.forward);
             
                
                competeur += Time.deltaTime;
                Vector3 newPos = transform.position + dir.normalized * (distanceAgent / (2 + dot));
                transform.position = Vector3.Lerp(transform.position, newPos, competeur);

            }

        }
        else
        {
            compteurZoomBullet += Time.deltaTime/speedZoomSpeed;
            transform.position = Vector3.Lerp(transform.position, basePosition, compteurZoomBullet);
            competeur = 0;
            compteurDezoomBullet = 0;
        }

    }
}

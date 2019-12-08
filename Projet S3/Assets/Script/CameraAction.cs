﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{

    private GameObject player;

    [Header("Bullet")]
    public float distanceOfStartDezoomBullet = 10;
    public float speedDezoomBullet;

    [Header("Agent")]
    public float distanceOfStartDezoomAgent = 20;
    public float speedDezoomAgent;

    [Header("Zoom")]
    public float speedZoomSpeed;

    [Header("Proposition")]
    public bool decalageScope;
    private bool supZero;
    private Vector3 ecartJoueur;
    private Vector3 basePosition;
    private MouseScope playerMouseScope;
    private EnnemiStock playerEnnemiStock;
    private float distanceBullet;
    private float distanceAgent;
    private float compteurDezoomBullet;
    private float compteurZoomBullet;
    private float competeur;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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
                if (distanceBullet > distanceOfStartDezoomBullet)
                {
                    compteurDezoomBullet += Time.deltaTime / speedDezoomBullet;
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

                Vector3 dir = playerEnnemiStock.ennemiStock.transform.position - player.transform.position;
                if (distanceAgent > distanceOfStartDezoomAgent)
                {

                    compteurDezoomBullet += Time.deltaTime / speedDezoomAgent;
                    compteurZoomBullet = 0;
                    Vector3 camPos = basePosition + -transform.forward * (distanceAgent - distanceOfStartDezoomAgent);
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
            if (decalageScope)
            {
                Vector3 dir = (playerMouseScope.direction + playerMouseScope.directionManette).normalized ;
                if (dir.z < 0)
                {
                    if (!supZero)
                    {
                        compteurZoomBullet = 0;
                        supZero = true;
                    }

                    Vector3 posCamPlus = dir * 20;
                    compteurZoomBullet += Time.deltaTime / speedZoomSpeed;
                    transform.position = Vector3.Lerp(transform.position, basePosition + posCamPlus, compteurZoomBullet);
                    competeur = 0;
                    compteurDezoomBullet = 0;
                }
                else
                {
                    if (supZero) {
                        compteurZoomBullet = 0;
                        supZero = false;
                    }
                    
                    compteurZoomBullet += Time.deltaTime / speedZoomSpeed;
                    transform.position = Vector3.Lerp(transform.position, basePosition, compteurZoomBullet);
                    competeur = 0;
                    compteurDezoomBullet = 0;
                }
            }
            else
            {
                compteurZoomBullet += Time.deltaTime / speedZoomSpeed;
                transform.position = Vector3.Lerp(transform.position, basePosition, compteurZoomBullet);
                competeur = 0;
                compteurDezoomBullet = 0;
            }
        }

    }
}

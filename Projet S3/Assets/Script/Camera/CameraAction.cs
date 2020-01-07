﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{

    [HideInInspector] public GameObject player;
    [Header("Profil")]
    public ProfilCamera profil;

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
    public float decalageCamera = 0;
    public float speedbase = 10;
    public float multiplicateurSpeedMax = 5;
    [Range(0, 1)]
    public float resetLerp = 0.3f;
    private Vector3 previousMousePos;

    public float distanceMax = 100;
    private bool supZero;
    [HideInInspector] public Vector3 ecartJoueur;
    [HideInInspector] public Vector3 basePosition;
    private MouseScope playerMouseScope;
    private EnnemiStock playerEnnemiStock;
    private float distanceBullet;
    private float distanceAgent;
    private float compteurDezoomBullet;
    private float compteurZoomBullet;
    private float competeur;
    public Camera orthoCam;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //transform.position = player.transform.position+ profil.position;
        //transform.eulerAngles = profil.rotation;
        ecartJoueur = transform.position - player.transform.position;
        playerMouseScope = player.GetComponent<MouseScope>();
        playerEnnemiStock = player.GetComponent<EnnemiStock>();
        previousMousePos = transform.position;
    }


    void Update()
    {
        orthoCam.orthographicSize = 15 * Vector3.Distance(transform.position, player.transform.position) / 25;

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
                    float currentDezoom = Mathf.Clamp(((distanceBullet - 10) / 1.5f), 0, distanceMax);
                    Vector3 camPos = basePosition + -transform.forward * currentDezoom;
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
                    Vector3 camPos = basePosition + -transform.forward * ((distanceAgent - distanceOfStartDezoomAgent) / 2);
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
                Vector3 dir = (playerMouseScope.direction + playerMouseScope.directionManette).normalized;

                if (dir.z < 1f)
                {
                    float test = dir.z - 1f;
                    test = Mathf.Clamp(test, -1, 1);
                    Vector3 currentDir = transform.position - basePosition;
                    currentDir = new Vector3(currentDir.x, 0, currentDir.z);

                    Vector3 posCamPlus = dir * decalageCamera * Mathf.Abs(test);
                    Vector3 newDir = (basePosition + posCamPlus) - basePosition;
                    Vector3 newPos = basePosition + posCamPlus;
                    newDir = new Vector3(newDir.x, 0, newDir.z);
                    float dot1 = Vector3.Dot(Vector3.forward, currentDir.normalized);
                    float dot2 = Vector3.Dot(Vector3.forward, newDir.normalized);


                    float distanceNextPos = Vector3.Distance(transform.position, basePosition + posCamPlus);
                    float distanceMouseDistance = Vector3.Distance(newPos, previousMousePos);

                    previousMousePos = newPos;

                    float speedGive = distanceNextPos / speedbase;
                    speedGive = Mathf.Clamp(speedGive, 0f, multiplicateurSpeedMax);
                    compteurZoomBullet += Time.deltaTime * speedGive;
                    transform.position = Vector3.Lerp(transform.position, basePosition + posCamPlus, compteurZoomBullet);


                    if (distanceNextPos < 3 || compteurZoomBullet > 1 || distanceNextPos > 3 && compteurZoomBullet > resetLerp && distanceMouseDistance > 0.02f)
                    {
                        compteurZoomBullet = 0;
                        supZero = true;
                    }
                    competeur = 0;
                    compteurDezoomBullet = 0;
                }
                else
                {
                    if (supZero)
                    {
                        compteurZoomBullet = 0;
                        supZero = false;
                    }
                    Vector3 currentDir = transform.position - basePosition;
                    currentDir = new Vector3(currentDir.x, 0, currentDir.z);

                    Vector3 newDir = dir;
                    newDir = new Vector3(newDir.x, 0, newDir.z);
                    float dot1 = Vector3.Dot(Vector3.forward, currentDir.normalized);
                    float dot2 = Vector3.Dot(Vector3.forward, newDir.normalized);

                    float distanceNextPos = Vector3.Distance(transform.position, basePosition);

                    float speedGive = distanceNextPos / speedbase;
                    compteurZoomBullet += Time.deltaTime * speedGive;

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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScope : MonoBehaviour
{
    public Material line;
    public GameObject bullet;
    public GameObject[] Ambout;
    public int numberAmbout;
    // public GameObject spawn;
    private EnnemiStock ennemiStock;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 directionManette;
    [HideInInspector] public GameObject instanceBullet;
    private LineRenderer lineRenderer;
    private bool returnLine;
    private bool destructBool;
    private Vector3 ballPos;
    private Vector3 returnPos;
    private float distanceReturn;
    private Vector3 dirReturn;
    public float returnSpeed = 50;
    [Header("Tirer Sound")]
    [FMODUnity.EventRef]
    public string contact;
    private FMOD.Studio.EventInstance contactSound;
    public float volume = 10;
    [Header("Retour Sound")]
    [FMODUnity.EventRef]
    public string returnSound;
    private FMOD.Studio.EventInstance returnEvent;
    public float returnVolume = 10;
    public float speedOfBullet;
    public float timerOfBullet;
    private float _timerOfBullet;
    private GameObject meshBullet;
    // Start is called before the first frame update
    void Start()
    {

        ennemiStock = GetComponent<EnnemiStock>();
        lineRenderer = GetComponent<LineRenderer>();
        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(volume);
        returnEvent = FMODUnity.RuntimeManager.CreateInstance(returnSound);
        returnEvent.setVolume(returnVolume);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (numberAmbout == Ambout.Length - 1)
            {
                numberAmbout = 0;
            }
            else
            {
                numberAmbout++;
            }
        }


        direction = DirectionSouris();
        directionManette = DirectionManette();
        if (directionManette != Vector3.zero)
        {
            direction = Vector3.zero;
        }
        float input = Input.GetAxis("Attract1");
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || input != 0)
        {

            if (ennemiStock.ennemiStock == null && instanceBullet == null)
            {

                instanceBullet = Instantiate(bullet, transform.position + (direction + directionManette) * 0.5f, Quaternion.identity);
                meshBullet = Instantiate(Ambout[numberAmbout], instanceBullet.transform.position, Quaternion.identity, instanceBullet.transform);
                float angle = Vector3.SignedAngle(transform.forward, (direction + directionManette).normalized, transform.up);

                Vector3 eulers = new Vector3(Ambout[numberAmbout].transform.eulerAngles.x, angle, Ambout[numberAmbout].transform.eulerAngles.z);
                meshBullet.transform.localRotation = Quaternion.Euler(eulers);

                _timerOfBullet = 0;


                Projectils projectils = instanceBullet.GetComponent<Projectils>();
                projectils.dir = (direction + directionManette).normalized;
                projectils.player = gameObject;
                projectils.lineRenderer = lineRenderer;
                speedOfBullet = projectils.speed;
                projectils.moveAlone = GetComponent<PlayerMoveAlone>();
                contactSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

                contactSound.start();
            }

        }
        if (ennemiStock.ennemiStock == null && instanceBullet != null)
        {

            Projectils projectils = instanceBullet.GetComponent<Projectils>();

            if (_timerOfBullet > timerOfBullet)
            {
                if (!projectils.returnBall)
                {
                    projectils.returnBall = true;
                    projectils.dir = -projectils.dir;


                }
                projectils.dir = transform.position - instanceBullet.transform.position;
                float angle = Vector3.SignedAngle(transform.forward, projectils.dir, transform.up);
                Vector3 eulers = new Vector3(meshBullet.transform.eulerAngles.x, angle, meshBullet.transform.eulerAngles.z);
                meshBullet.transform.localRotation = Quaternion.Euler(eulers);

            }
            else
            {
                _timerOfBullet += Time.deltaTime;
            }
            returnLine = true;
            destructBool = false;

            ballPos = instanceBullet.transform.position;

        }

        if (ennemiStock.ennemiStock == null && instanceBullet == null)
        //Return de line renderer après le tir;
        {
            if (returnLine)
            {
                if (!destructBool)
                {
                    returnPos = ballPos;
                    returnEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

                    returnEvent.start();
                    destructBool = true;
                }
                if (Vector3.Distance(transform.position, returnPos) > 3)
                {

                    float dis = Vector3.Distance(transform.position, returnPos);
                    dirReturn = transform.position - returnPos;


                    if (dis > returnSpeed)
                    {
                        returnPos += dirReturn.normalized * dis * Time.deltaTime;

                    }
                    else
                    {
                        returnPos += dirReturn.normalized * returnSpeed * Time.deltaTime;
                    }

                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, returnPos);
                }
                else
                {
                    returnLine = false;
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                    }

                }
            }
        }
        if (ennemiStock.ennemiStock == null && instanceBullet != null)
        {
            Projectils projectils = instanceBullet.GetComponent<Projectils>();

            if (Vector3.Distance(transform.position, instanceBullet.transform.position) < 5 && projectils.returnBall)
            {
                Destroy(instanceBullet);
            }
        }
    }

    private void OnRenderObject()
    {

        if (ennemiStock.ennemiStock == null && instanceBullet == null)
        {
            GL.Begin(GL.LINES);
            line.SetPass(0);
            GL.Color(Color.red);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + (direction + directionManette) * 100);
            GL.End();
        }

    }

    private void OnDrawGizmos()
    {
        GL.Begin(GL.LINES);
        line.SetPass(0);
        GL.Color(Color.red);
        GL.Vertex(transform.position);
        GL.Vertex(transform.position + (direction + directionManette) * 100);
        GL.End();
    }
    private Vector3 DirectionSouris()
    {
        Ray camera = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        float rauEnter;

        if (ground.Raycast(camera, out rauEnter))
        {
            Vector3 pointToLook = camera.GetPoint(rauEnter);
            Vector3 posPlayer = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 dir = pointToLook - posPlayer;



            return dir;
        }
        else
        {
            return Vector3.zero;
        }
    }
    private Vector3 DirectionManette()
    {
        float aimHorizontal = Input.GetAxis("AimHorizontal1");
        float aimVertical = -Input.GetAxis("AimVertical1");

        Vector3 dir = new Vector3(aimHorizontal, 0, aimVertical);
        return dir.normalized;
    }
}

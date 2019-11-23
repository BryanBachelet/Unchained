﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRend : MonoBehaviour
{
    public bool upProjection = true;
    [HideInInspector]
    public bool active;
    public LineRenderer lineRenderer;
    public BoxCollider box;
    private GameObject p1;
    private GameObject p2;
    private float dot;
    private float distance;
    private EnnemiStock ennemiStock;

    [Header("Sound")]
    [FMODUnity.EventRef]
    public string contact;
    private FMOD.Studio.EventInstance contactSound;
   
    public float volume = 20;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.GetComponent<EnnemiStock>())
        {
            ennemiStock = transform.parent.GetComponent<EnnemiStock>();
        }
        //Sound
        contactSound = FMODUnity.RuntimeManager.CreateInstance(contact);
        contactSound.setVolume(volume);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        contactSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        if (ennemiStock != null)
        {
            if (ennemiStock.ennemiStock != null)
            {
                box.enabled = true;
                active = true;
                p2 = ennemiStock.ennemiStock;
                p1 = transform.parent.gameObject;
            }
            else
            {
                active = false;
                box.enabled = false;
            }
        }

        if (active)
        {
            SetLine();
            ColliderSize();

        }
    }

    void SetLine()
    {
        lineRenderer.SetPosition(0, p2.transform.localPosition);
        lineRenderer.SetPosition(1, p1.transform.localPosition);

    }

    void ColliderSize()
    {
        distance = Vector3.Distance(p1.transform.position, p2.transform.position);
        Vector3 dir = p2.transform.position - p1.transform.position;
        dot = Vector3.SignedAngle(dir.normalized, Vector3.forward, Vector3.up);
        transform.position = p2.transform.position + (-dir.normalized * distance) / 2;
        transform.rotation = Quaternion.Euler(0, -dot, 0);
        box.size = new Vector3(1, 1, distance);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Ennemi")
        {
            if (!upProjection)
            {
                float sign = Mathf.Sign(Vector3.Angle(transform.position, collision.transform.position));
                collision.GetComponent<Rigidbody>().AddForce(sign * transform.right * 50, ForceMode.Impulse);
            }
            else
            {
                contactSound.start();
                collision.GetComponent<Rigidbody>().AddForce(Vector3.up* 50, ForceMode.Impulse);
            }
            collision.GetComponent<EnnemiDestroy>().isDestroying = true;
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiDestroy : MonoBehaviour
{
    public bool isDestroying;
    public float timerToDestro = 2;
    private float compteur;
    bool enter = false;
    public GameObject vfxBlueUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroying)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (isDestroying)
            {
                if (!enter)
                {
                    if (vfxBlueUp != null)
                    {
                        Instantiate(vfxBlueUp, transform.position, transform.rotation, Camera.main.transform);
                    }
                    enter = true;
                }
                if (compteur > timerToDestro)
                {

                    Destroy(gameObject);
                    //if(vfxBlueUp != null)
                    //{


                    //}

                }
                else
                {
                    compteur += Time.deltaTime;
                }
            }

        }
    }
}

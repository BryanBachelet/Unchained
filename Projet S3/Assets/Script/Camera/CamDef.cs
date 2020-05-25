using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colorful;

public class CamDef : MonoBehaviour
{
    public LifePlayer life;

    public float distancePlayer;

    public float timing =1f;

    public float angleSpeed = 60;
    private bool activeCame;

    private float currentDist;

    private Vector3 dirCam;

    private float compteur =0; 

    private GameObject player;

    private Colorful.Threshold threshold;

    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        threshold = GetComponent<Threshold>();
        player = PlayerMoveAlone.Player1;
    }

    // Update is called once per frame
    void Update()
    {
      if(life.deathState)
      {
            if(!activeCame)
            {
                currentDist =  Vector3.Distance(transform.position,PlayerMoveAlone.Player1.transform.position);
                dirCam =  player.transform.position - transform.position;
                activeCame =true;
                threshold.enabled = true;
            }

            float ratio= compteur/timing;
            Vector3 pos = player.transform.position+ ((Quaternion.Euler(0,angle,0)  * -dirCam.normalized) *distancePlayer);
            transform.position = transform.position + Quaternion.Euler(0,angle,0) *transform.forward;
            transform.position = Vector3.Lerp(transform.position,pos,ratio);
            transform.LookAt(player.transform);
            compteur +=Time.deltaTime;
            angle += angleSpeed*Time.deltaTime;

      }   
    }
}

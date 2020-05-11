using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{

    [HideInInspector] public GameObject player;

    [HideInInspector] public Vector3 ecartJoueur;
    public bool activeBehavior;
    [HideInInspector] public Vector3 basePosition;

    public Camera orthoCam;

    private Vector3 rotAtStart;

    public float speed1 = 5;


    public bool changeStyle;
    public float speed2 = 100;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerMoveAlone.Player1;


        ecartJoueur = transform.position - player.transform.position;
        rotAtStart = transform.eulerAngles;

    }


    public void ResetPos()
    {
        ecartJoueur = transform.position - player.transform.position;
        activeBehavior = false;
    }

    public void Deactive(bool right)
    {
        activeBehavior = true;
        if (right)
        {
            rotAtStart = new Vector3(rotAtStart.x, 0, 0);

        }
        else
        {

            rotAtStart = new Vector3(rotAtStart.x, 180, 0);
        }
    }


    void Update()
    {
        orthoCam.orthographicSize = 15 * Vector3.Distance(transform.position, player.transform.position) / 25;

        if (!activeBehavior)
        {
            basePosition = player.transform.position + ecartJoueur;
            if(changeStyle)
            {
                transform.position = Vector3.MoveTowards(transform.position,basePosition,speed2*Time.deltaTime); // Vector3.Lerp(transform.position, basePosition,speed* Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, basePosition,speed1* Time.deltaTime);
            }
            transform.eulerAngles = rotAtStart;
        }


    }

}

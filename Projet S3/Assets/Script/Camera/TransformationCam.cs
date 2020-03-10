using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationCam : MonoBehaviour
{
    public GameObject player;
    public CamMouvement cam;
    public float speedOfRotation;
    public GameObject startPos;


    private Vector3 basePos;
    private Vector3 dir;
    private float dist;
    private bool activeBehavior;
    private float angleCompteur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cam.i == cam.cams.Count && StateOfGames.currentState == StateOfGames.StateOfGame.Transformation)
        {
            if (!activeBehavior)
            {
                basePos = startPos.transform.position;
                dir = (transform.position - player.transform.position).normalized;
                dist = (transform.position - player.transform.position).magnitude;
                activeBehavior = true;
            }
            Vector3 nextPos = player.transform.position + Quaternion.Euler(0, angleCompteur, 0) * (dir * dist);
            transform.position = Vector3.Lerp(transform.position, nextPos, 1);
            transform.LookAt(player.transform.position);
            angleCompteur += speedOfRotation * Time.deltaTime;
            angleCompteur = Mathf.Clamp(angleCompteur, 0, 5);
        }
        
    }
}

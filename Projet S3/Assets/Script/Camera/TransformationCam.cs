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

    public float shake_decay = 0;
    public float shake_intensity = 0;

    Quaternion originRotation;
    public float clampValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("test");
        if(cam.i == cam.cams.Count && StateOfGames.currentState == StateOfGames.StateOfGame.Transformation)
        {
            originRotation = transform.rotation;
            if (!activeBehavior)
            {
                basePos = startPos.transform.position;
                dir = (transform.position - player.transform.position).normalized;
                dist = (transform.position - player.transform.position).magnitude;
                activeBehavior = true;
            }
            if (shake_intensity > 0)
            {
                transform.rotation = Quaternion.Euler(originRotation.x + Random.Range(-shake_intensity, shake_intensity) * 0.2f,
                                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * 0.2f,
                                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * 0.2f);
                shake_intensity -= shake_decay;
            }
            Shake();
            Vector3 nextPos = player.transform.position + Quaternion.Euler(0, angleCompteur, 0) * (dir * dist);
            transform.position = Vector3.Lerp(transform.position, nextPos, 1);
            transform.LookAt(player.transform.position);
            angleCompteur += speedOfRotation * Time.deltaTime;
            angleCompteur = Mathf.Clamp(angleCompteur, 0, clampValue);

        }
        
    }
    void Shake()
    {
        originRotation = transform.rotation;
    }
}

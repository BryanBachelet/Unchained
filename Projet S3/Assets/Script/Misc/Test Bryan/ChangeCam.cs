using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject player;

    public float time;


    public bool right;
    public bool active;
    private float compteur;
    private CameraAction cameraAction;

    private Vector3 posStart;
    private Vector3 rotStart;
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 rotB;



    void Start()
    {
        cameraAction = GetComponent<CameraAction>();

        posStart = transform.position - player.transform.position;
        rotStart = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        posA = player.transform.position + posStart;
        posB = player.transform.position + Quaternion.Euler(0, 180, 0) * posStart;
        rotB = rotStart + new Vector3(0, 180, 0);

        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            right = !right;
                cameraAction.Deactive(!right);
            Debug.Log(right + " / " + !right);
            active = true;
        }
        if (active)
        {

            if (right)
            {
                float compteurIncrease = compteur / time;
                transform.position = Vector3.Lerp(posA, posB, compteurIncrease);
                transform.eulerAngles = Vector3.Lerp(rotStart, rotB, compteurIncrease);
                transform.LookAt(player.transform);
                compteur += Time.deltaTime;
                compteurIncrease = Mathf.Clamp(compteurIncrease, 0, 1f);
                if (compteurIncrease >= 1)
                {
                    cameraAction.ResetPos();
                    active = false;
                }
            }
            else
            {
              
                float compteurIncrease = compteur / time;
                transform.position = Vector3.Lerp(posA, posB, compteurIncrease);
                transform.eulerAngles = Vector3.Lerp(rotStart, rotB, compteurIncrease);
                transform.LookAt(player.transform);
                compteur -= Time.deltaTime;
                compteurIncrease = Mathf.Clamp(compteurIncrease, 0, 1f);
                if (compteur <= 0)
                {
                    cameraAction.ResetPos();
                    active = false;
                }
            }
        }
    }
}

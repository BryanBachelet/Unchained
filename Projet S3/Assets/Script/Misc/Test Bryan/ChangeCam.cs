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
    private bool active;
    private float compteur;
    private CameraAction cameraAction;

    void Start()
    {
        cameraAction = GetComponent<CameraAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            right = !right;
            cameraAction.Deactive();
            active = true;
        }
        if (active)
        {

            if (right)
            {
                float compteurIncrease = compteur / time;
                transform.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, compteurIncrease);
                transform.eulerAngles = Vector3.Lerp(pointA.transform.eulerAngles, pointB.transform.eulerAngles, compteurIncrease);
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
                transform.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, compteurIncrease);
                transform.eulerAngles = Vector3.Lerp(pointA.transform.eulerAngles, pointB.transform.eulerAngles, compteurIncrease);
                transform.LookAt(player.transform);
                compteur += Time.deltaTime;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_visée : MonoBehaviour
{
    public float position = 10;
    public bool visee;
    public MouseScope mouse;
    public RotationPlayer rotation;
    public MeshRenderer mesh;
    public Material aimMaterial;
    public Material rotateMaterial;


    // Start is called before the first frame update
    void Start()
    {
        mouse = GetComponentInParent<MouseScope>();
        rotation = GetComponentInParent<RotationPlayer>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (StateAnim.state != StateAnim.CurrentState.Rotate)
        {
            mesh.material = aimMaterial;
            transform.position = transform.parent.position + mouse.direction.normalized * position;

            float angleConversion = transform.eulerAngles.y;
            angleConversion = angleConversion > 180 ? angleConversion - 360 : angleConversion;
            float angleAvatar = Vector3.SignedAngle(Vector3.forward, mouse.direction.normalized, Vector3.up) ;
            if (angleConversion < 0 && angleAvatar == 180)
            {
                angleAvatar = -180;
            }
            transform.eulerAngles = new Vector3(0, angleAvatar, 0);
        }


        if (StateAnim.state == StateAnim.CurrentState.Rotate)
        {

            mesh.material = rotateMaterial;
            transform.position = transform.parent.position + rotation.nextDir.normalized * position;

            float angleConversion = transform.eulerAngles.y;
            angleConversion = angleConversion > 180 ? angleConversion - 360 : angleConversion;
            float angleAvatar = Vector3.SignedAngle(Vector3.forward, rotation.nextDir.normalized, Vector3.up) ;
            if (angleConversion < 0 && angleAvatar == 180)
            {
                angleAvatar = -180;
            }
            transform.eulerAngles = new Vector3(0, angleAvatar, 0);
        }

    }
}



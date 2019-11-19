using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrap : MonoBehaviour
{
    public float dot;
    public float cameraOffset= 10;
    Vector3 startPos;
    EnnemiStock ennemiStock;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        ennemiStock = GetComponentInParent<EnnemiStock>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ennemiStock.ennemiStock != null)
        {
            float t = Time.deltaTime;
            GameObject target = ennemiStock.ennemiStock;

            Vector3 dir = target.transform.position - transform.position;
            dir = new Vector3(dir.x, 0, dir.z);
             dot = -Vector3.Dot(dir.normalized,transform.parent.forward);
           Vector3 newPos  = transform.parent.position + startPos + dir.normalized * (cameraOffset*(dot+1f));
            transform.position = Vector3.Lerp(transform.position, newPos, t);
        }
        else
        {
            float t = Time.deltaTime;
            Vector3 newPos = transform.parent.position + startPos;
            transform.position = Vector3.Lerp(transform.position, newPos, t);
            
        }
    }
}

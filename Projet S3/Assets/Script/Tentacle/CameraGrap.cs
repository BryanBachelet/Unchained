using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrap : MonoBehaviour
{
    private float dot;
  &
    public float ennemiSpeedDezoom = 10;
    public float bulletSpeedDezoom = 2;
    public float speedZoom = 2;
    Vector3 startPos;
    EnnemiStock ennemiStock;
    bool change;
    float t;
    float i;
    float k;
    MouseScope mouseScope;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        ennemiStock = GetComponentInParent<EnnemiStock>();
        mouseScope = GetComponentInParent<MouseScope>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ennemiStock.ennemiStock != null)
        {
            k = 0;
            t += Time.deltaTime / ennemiSpeedDezoom;
            GameObject target = ennemiStock.ennemiStock;

            Vector3 dir = target.transform.position - transform.parent.position;
            float distance = Vector3.Distance(transform.parent.position, target.transform.position);
            Debug.Log(distance);
            if (distance > 25)
            {
                i += Time.deltaTime;

                Vector3 camPos = transform.parent.position + startPos + -transform.forward * (distance - 25);
                transform.position = Vector3.Lerp(transform.position, camPos, i);


            }
            else
            {
                Vector3 CamPos = transform.parent.position + startPos;
                transform.position = Vector3.Lerp(transform.position, CamPos, t);
            }


            dir = new Vector3(dir.x, 0, dir.z);
            dot = -Vector3.Dot(dir.normalized, transform.parent.forward);
            dot = dot - 1;

            Vector3 newPos = transform.parent.position + startPos + dir.normalized * (distance / 2);
            transform.position = Vector3.Lerp(transform.position, newPos, t);

        }

        else
        {
            t = 0;
            if (mouseScope.instanceBullet != null)
            {
                k += Time.deltaTime / bulletSpeedDezoom;
                float distance = Vector3.Distance(transform.parent.position, mouseScope.instanceBullet.transform.position);
                if (distance > 25)
                {
                    Vector3 camPos = transform.parent.position + startPos + -transform.forward * ((distance - 25) / 2);
                    transform.position = Vector3.Lerp(transform.position, camPos, k);
                }
                else
                {
                    Vector3 CamPos = transform.parent.position + startPos;
                    transform.position = Vector3.Lerp(transform.position, CamPos, k);
                }
            }
            else
            {
                k = 0;
                i = 0;
                t = speedZoom * Time.deltaTime;
                Vector3 newPos = transform.parent.position + startPos;
                transform.position = Vector3.Lerp(transform.position, newPos, t);
            }

        }

    }
}


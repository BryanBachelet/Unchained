using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTiling : MonoBehaviour
{
    public Transform bulletTransform;
    public SpriteRenderer mySR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletTransform != null)
        {
            transform.rotation = Quaternion.Euler(90, 0, Vector3.Angle(Vector3.forward, bulletTransform.position));
            mySR.size = new Vector2(Vector3.Distance(transform.position, bulletTransform.position), 10.24f);
        }
        else
        {
            mySR.size = new Vector2(3,10.24f);
        }
    }
}

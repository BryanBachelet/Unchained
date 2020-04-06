using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MyWorldPos : MonoBehaviour
{
    public Vector3 mypos;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        mypos = transform.position;
        Shader.SetGlobalVector("_CoeurPos", mypos);
    }
}

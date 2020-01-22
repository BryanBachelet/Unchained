using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColor : MonoBehaviour
{
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInParent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

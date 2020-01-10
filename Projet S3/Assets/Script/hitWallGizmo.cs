using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitWallGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.up * 100,Color.blue);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, Vector3.up * Mathf.Infinity);
    }
}

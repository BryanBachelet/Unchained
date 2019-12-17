using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionByWall : MonoBehaviour
{
    public float radius;
    public List<GameObject> detected = new List<GameObject>();
    public bool check;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool launcDetection(bool isDetected)
    {
        Collider[] grounddetected = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < grounddetected.Length; i++)
        {
            if (gameObject != grounddetected[i].gameObject)
            {
                detected.Add(grounddetected[i].transform.parent.gameObject);
            }

        }
        isDetected = true;
        return isDetected;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }


}

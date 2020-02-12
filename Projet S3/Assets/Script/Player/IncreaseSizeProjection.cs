using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSizeProjection : MonoBehaviour
{
    public float radiusCollider;
    public CapsuleCollider capsuleCollider;

    public void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        if(StateAnim.state == StateAnim.CurrentState.Projection)
        {
            capsuleCollider.radius = radiusCollider;
            capsuleCollider.height = 4 * radiusCollider;
            capsuleCollider.center = Vector3.zero + Vector3.up;
        }
        else
        {
            capsuleCollider.radius = 0.5f;
            capsuleCollider.height = 4 * 0.5f;
            capsuleCollider.center = Vector3.zero;
        }
    }
}

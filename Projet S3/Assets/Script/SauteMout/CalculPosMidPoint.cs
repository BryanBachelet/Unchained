using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculPosMidPoint : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float Dist;
    public Vector3 dir;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        dir = PlayerCommands.DirectionBetweenPlayer();
        Dist = Vector3.Distance(player1.position, player2.position);
        transform.position = player1.position + (dir.normalized * Dist) / 2;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneR : MonoBehaviour
{
    private float distance;
    private float dot;
    public GameObject plane;
    public float timeActive;
    private float compteur;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (plane.SetActive(true))
        //{

        //}
    }
    public void CreatePlane()
    {
        distance = Vector3.Distance(PlayerCommands.player1.transform.position, PlayerCommands.player2.transform.position);
        Vector3 dir = PlayerCommands.DirectionBetweenPlayer();
        dot = Vector3.SignedAngle(dir.normalized, Vector3.forward, Vector3.up);
        plane.transform.position = PlayerCommands.player2.transform.position + (-PlayerCommands.DirectionBetweenPlayer() * distance) / 2;
        plane.transform.position = new Vector3(plane.transform.position.x, 0.1f, plane.transform.position.z);
        plane.transform.rotation = Quaternion.Euler(0, -dot, 0);
        plane.transform.localScale = new Vector3(0.5f, 0.5f, distance / 10);
        plane.SetActive(true);
    }
    public void DestroyPlane()
    {
        plane.SetActive(false);
    }

}

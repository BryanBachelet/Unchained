using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculPosMidPoint : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    float posX;
    float posY;
    float posZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = (player1.position - (player1.position - player2.position) / 2) + (player1.position - (player1.position - player2.position) * (0.50f + (0.05f * VitesseFunction.currentLv)));
        posX = (player1.position.x - (player1.position.x - player2.position.x) / 2) + (player1.position.x - (player1.position.x - player2.position.x) * (0.50f + (0.05f * VitesseFunction.currentLv)));
        posY = (player1.position.y - (player1.position.y - player2.position.y) / 2) + (player1.position.y - (player1.position.y - player2.position.y) * (0.50f + (0.05f * VitesseFunction.currentLv)));
        posX = (player1.position.z - (player1.position.z - player2.position.z) / 2) + (player1.position.z - (player1.position.z - player2.position.z) * (0.50f + (0.05f * VitesseFunction.currentLv)));
        transform.position = new Vector3(posX, posY, posZ);
    }
}

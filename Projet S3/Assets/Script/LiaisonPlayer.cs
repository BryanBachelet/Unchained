using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiaisonPlayer : MonoBehaviour
{
    private LineRenderer linePlayers;

    private GameObject player1;
    private GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        linePlayers = GetComponent<LineRenderer>();
        player1 = PlayerCommands.player1;
        player2 = PlayerCommands.player2;
    }

    // Update is called once per frame
    void Update()
    {
        linePlayers.SetPosition(0, player1.transform.position);
        linePlayers.SetPosition(1, player2.transform.position);
    }
}

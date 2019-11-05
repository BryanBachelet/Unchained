using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand : MonoBehaviour
{
    public static GameObject player1Static;
    public static GameObject player2Static;

    public GameObject player1;
    public GameObject player2;


    private void Awake()
    {
        player1Static = player1;
        player2Static = player2;
    }

    public static void ChangeStatePlayer(GameObject player, PlayerState.PlayerCurrentState newState, Color color)
    {
        Renderer rendPlayer = player.GetComponent<Renderer>();
        rendPlayer.material.color = color;

        PlayerState playerState = player.GetComponent<PlayerState>();
        playerState.currentState = newState;
    }
    public static void ChangeStatePlayer(GameObject player, PlayerState.PlayerCurrentState newState)
    {
        PlayerState playerState = player.GetComponent<PlayerState>();
        playerState.currentState = newState;
    }
    public static bool CheckState(GameObject player, PlayerState.PlayerCurrentState currentStatePlayer)
    {
        PlayerState playerState = player.GetComponent<PlayerState>();
        if (playerState.currentState == currentStatePlayer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Vector3 DirectionBetweenPlayer()
    {
        Vector3 dir = player1Static.transform.position - player2Static.transform.position;
        return dir.normalized;
    }
    public static float DistanceBetweenPlayer()
    {
        float distance = Vector3.Distance(player1Static.transform.position, player2Static.transform.position);
        return distance;
    }

}

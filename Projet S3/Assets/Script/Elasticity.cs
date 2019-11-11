using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elasticity : MonoBehaviour
{
    [Header("Variable Function")]
    public float elasticityMin;
    public float MaxPlayerResistance;
    public float ratioOfElasticityForce;
    public float gainDeplayerResistance;
    public float RatioSpeed; 
    [Header("Current Variable")]
    public float playerResistance;
    public float distancePlayer;


    private Rigidbody rigid1;
    private Rigidbody rigid2;


    // Start is called before the first frame update
    void Start()
    {
        rigid1 = PlayerCommands.player1.GetComponent<Rigidbody>();
        rigid2 = PlayerCommands.player2.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        distancePlayer = Vector3.Distance(PlayerCommands.player1.transform.position, PlayerCommands.player2.transform.position);
        if (playerResistance < MaxPlayerResistance)
        {
            playerResistance += gainDeplayerResistance * (1 / distancePlayer);
        }
        if (distancePlayer < elasticityMin)
        {
            PlayerCommands.ChangePlayerState(PlayerCommands.player1, PlayerState.StateOfPlayer.Free);
            PlayerCommands.ChangePlayerState(PlayerCommands.player2, PlayerState.StateOfPlayer.Free);
        }

        if (distancePlayer > elasticityMin)
        {

            if (playerResistance > 0)
            {
                playerResistance -= ratioOfElasticityForce * (distancePlayer - elasticityMin);
            }
            if (playerResistance <= 0)
            {
                PlayerCommands.ChangePlayerState(PlayerCommands.player1, PlayerState.StateOfPlayer.ElastycityMouvement);
                PlayerCommands.ChangePlayerState(PlayerCommands.player2, PlayerState.StateOfPlayer.ElastycityMouvement);
                ElasticPlayer(PlayerCommands.player2, rigid2);
                ElasticPlayer(PlayerCommands.player1, rigid1);
            }
        }
    }

    void ElasticPlayer(GameObject player, Rigidbody rigid)
    {

        GameObject otherPlayer = PlayerCommands.OtherPlayer(player);
        Vector3 dir = otherPlayer.transform.position - player.transform.position;
        rigid.velocity = dir.normalized * (RatioSpeed*distancePlayer) ;
    }
}

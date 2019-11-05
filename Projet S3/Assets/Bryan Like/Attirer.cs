using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attirer : MonoBehaviour
{
    public float stun;
    public float compteur;


    void Update()
    {
        if (PlayerCommand.CheckState(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Stun))
        {
            if (compteur > stun)
            {

                PlayerCommand.ChangeStatePlayer(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Libre, Color.blue);
                compteur = 0;
            }
            else
            {
                compteur += Time.deltaTime;
            }

        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ennemi" && PlayerCommand.CheckState(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Vol))
        {
           
            collision.rigidbody.AddForce(Vector3.up * 50, ForceMode.Impulse);
            // Destroy(collision.gameObject);
        }
    }
}

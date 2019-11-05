using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vol : MonoBehaviour
{
    public float flightTime;
    public Collider p1;
    public Collider childp2;
    private float compteur;
    PlayerState playerState;
    Renderer playerRender;
    PlayerController playerController;
    private Rigidbody rigid;
    public GameObject fx_signe_attirer;

    public Vector3 v;
    // Start is called before the first frame update
    void Start()
    {
        this.playerState = GetComponent<PlayerState>();
        this.playerRender = GetComponent<Renderer>();
        this.playerController = GetComponent<PlayerController>();
        this.rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.currentState == PlayerState.PlayerCurrentState.Vol)
        {


            if (compteur > flightTime)
            {

                Collider p2 = GetComponent<Collider>();

                Physics.IgnoreCollision(p1, p2, false);
                Physics.IgnoreCollision(childp2, p2, false);
                PlayerCommand.ChangeStatePlayer(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Libre, Color.red);
                compteur = 0;
            }
            if (Input.GetKeyDown("joystick " + playerController.playerButton.ToString() + " button 0"))
            {
                PlayerCommand.ChangeStatePlayer(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Vol, Color.cyan);
                Collider p2 = GetComponent<Collider>();
                Physics.IgnoreCollision(p1, p2, false);
                Physics.IgnoreCollision(childp2, p2, false);
                PlayerCommand.ChangeStatePlayer(PlayerCommand.player1Static, PlayerState.PlayerCurrentState.Attireur, Color.grey);
                //Ici, supprimer le fx qui à été instantié dans le script "jet";
                compteur = 0;
            }
            if (compteur / flightTime < 0.8f)
            {
                transform.position += v;
            }

            compteur += Time.deltaTime;

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ennemi" && playerState.currentState == PlayerState.PlayerCurrentState.Vol)
        {
            collision.rigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);
            //Destroy(collision.gameObject);
        }
    }
}

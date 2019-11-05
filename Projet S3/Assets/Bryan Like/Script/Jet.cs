using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{


    Renderer playerRender;
    PlayerController playerController;
    public float ratioSpeed = 2;
    Collider ballCollider;
    public Transform play1;
    public float speed;
    public float ratioTransmit;
    Vector3 scope;
    public GameObject fx_signe_attirer;
    // Start is called before the first frame update
    void Start()
    {
        ballCollider = GetComponent<Collider>();

        this.playerRender = GetComponentInParent<Renderer>();
        this.playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("V1");
        float verti = Input.GetAxis("V2");

        scope = new Vector3(hori, 0, verti).normalized;

        if (scope != Vector3.zero)
        {
            if (PlayerCommand.CheckState(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Libre))
            {

                PlayerCommand.ChangeStatePlayer(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Jet, Color.green);
                ballCollider.enabled = true;
                Physics.IgnoreCollision(play1.GetComponent<Collider>(), GetComponent<Collider>(), false);
               
            }

        }
        if (scope == Vector3.zero)
        {
            if (PlayerCommand.CheckState(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Jet))
            {
                PlayerCommand.ChangeStatePlayer(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Libre, Color.blue);

            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerCommand.CheckState(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Jet))
        {
            collision.gameObject.GetComponent<PlayerState>().currentState = PlayerState.PlayerCurrentState.Vol;
            collision.gameObject.transform.localPosition = transform.parent.transform.localPosition + new Vector3(1.5f, 0, 0);
            Projection(collision.gameObject.GetComponent<Rigidbody>(), collision, collision.transform.GetComponent<PlayerController>());
        }
    }


    void Projection(Rigidbody rigidbody, Collider p1, PlayerController playerConoller)
    {
        ballCollider = GetComponent<Collider>();
        Collider p2Parent = transform.parent.GetComponent<Collider>();
        Physics.IgnoreCollision(p1, p2Parent, true);

        Physics.IgnoreCollision(p1, ballCollider, true);
        
        (speed, ratioTransmit) = rigidbody.GetComponent<VitesseFunc>().changeVelocity(true, speed, ratioTransmit);
        rigidbody.GetComponent<Vol>().v = scope.normalized * speed * Time.deltaTime;
        rigidbody.GetComponent<Renderer>().material.color = Color.yellow;
        Instantiate(fx_signe_attirer, PlayerCommand.player1Static.transform.position, Quaternion.Euler(-270, 0, 0), PlayerCommand.player1Static.transform);

    }


    private void OnDrawGizmos()
    {
        if (PlayerCommand.CheckState(PlayerCommand.player2Static, PlayerState.PlayerCurrentState.Jet))
        {
            Gizmos.color = Color.magenta;
            Vector3 dir = scope.normalized;


            if (dir != Vector3.zero)
            {
                Gizmos.DrawRay(transform.position, dir * 100);
            }
        }
    }
}

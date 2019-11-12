using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    [Header("Deplacement")]
    public GameObject startPoint;
    public GameObject finishPoint;
    public float speed;

    public enum StateOfPayload { Good, Even, Bad };
    [Header("Capture")]
    public StateOfPayload state;

    public int playerIn;
    public int agentIn;
    public List<GameObject> GoCapture;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerIn >= 1 && agentIn >= 2)
        {
            state = StateOfPayload.Even;
        }
        if (playerIn < 1 && agentIn < 2)
        {
            state = StateOfPayload.Even;
        }
        if (playerIn >= 1 && agentIn < 2)
        {
            state = StateOfPayload.Good;
        }
        if (playerIn < 1 && agentIn >= 2)
        {
            state = StateOfPayload.Bad;
        }

        if (state == StateOfPayload.Good)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPoint.transform.position, speed * Time.deltaTime);
        }
        if (state == StateOfPayload.Bad)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, speed * Time.deltaTime);
        }
        if (GoCapture.Count > 0)
        {
            for (int i = 0; i < GoCapture.Count; i++)
            {

                if (GoCapture[i].tag != "Player" && GoCapture[i].tag != "Ennemi")
                {
                    GoCapture.RemoveAt(i);
                    agentIn--;
                }
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GoCapture.IndexOf(other.gameObject) == -1)
            {
                GoCapture.Add(other.gameObject);
                playerIn++;
            }
        }
        if (other.tag == "Ennemi")
        {
            if (GoCapture.IndexOf(other.gameObject) == -1)
            {
                GoCapture.Add(other.gameObject);
                agentIn++;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            int i = GoCapture.IndexOf(other.gameObject);

            GoCapture.RemoveAt(i);
            playerIn--;
        }
        if (other.tag == "Ennemi")
        {
            int i = GoCapture.IndexOf(other.gameObject);

            GoCapture.RemoveAt(i);
            agentIn--;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}

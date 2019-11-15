using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    [Header("Deplacement")]
    private GameObject startPoint;
    private GameObject finishPoint;
    public GameObject[] wayPoints;
    public float normalSpeed;
    public float ennemiSpeed;
    private int i;
    public enum StateOfPayload { Good, Bad };
    [Header("Capture")]
    public StateOfPayload state;


    private int agentIn;
    [HideInInspector] public List<GameObject> GoCapture;

    private void Start()
    {
        startPoint = wayPoints[0];
        finishPoint = wayPoints[1];
    }

    void Update()
    {
        if (agentIn < 1)
        {
            state = StateOfPayload.Good;
        }
        if (agentIn >= 1)
        {
            state = StateOfPayload.Bad;
        }

        if (state == StateOfPayload.Good)
        {
            transform.position = Vector3.MoveTowards(transform.position, finishPoint.transform.position, normalSpeed * Time.deltaTime);
        }
        if (state == StateOfPayload.Bad)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, ennemiSpeed * Time.deltaTime);
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
        ChangePosition();

    }

    public void ChangePosition()
    {
        float distFinish = Vector3.Distance(transform.position, finishPoint.transform.position);
        
        if (distFinish < 1f)
        {
            startPoint = finishPoint;
            i++;
            if (i == wayPoints.Length)
            {
                i = 0;
            }
            finishPoint = wayPoints[i];

        }


    }



    public void OnTriggerEnter(Collider other)
    {

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

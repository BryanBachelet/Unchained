using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashingTrans : MonoBehaviour
{
    public int numberToAim;
    public int numberInput;
    List<float> i = new List<float>();
    public float timing;

    public Text text;
    public CamMouvement camMouvement;

    private float compteur;
    private TransformationAgent agentTransfo;
    private ResetPlayer resetPlayerScript;
    private bool activeExplode;

    Collider[] hitColliders;
    void Start()
    {
        resetPlayerScript = GetComponent<ResetPlayer>();
        agentTransfo = GetComponent<TransformationAgent>();
    }

    void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 3);
        
        for(int j = 0; j < i.Count; j++)
        {
            if (i[j] + 1 < Time.time)
            {
                i.RemoveAt(j);
            }

        }
        numberInput = i.Count;
        numberToAim = hitColliders.Length;
        if (camMouvement.i >= camMouvement.cams.Count)
        {
            if(hitColliders.Length > 7)
            {
                compteur += Time.deltaTime;
            }
                text.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                i.Add(Time.time);
            }
            agentTransfo.startTranformationAnim(timing);
            if (i.Count < hitColliders.Length / 3 && compteur > 0.5f)
            {
                text.gameObject.SetActive(false);
                resetPlayerScript.ResetFonction(true);
            }
            else if(i.Count > hitColliders.Length / 1.2 && hitColliders.Length > 7)
            {
                Debug.Log("Win");
                if (!activeExplode)
                {
                    text.gameObject.SetActive(false);
                    agentTransfo.ExploseAgent();
                    activeExplode = true;
                    StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;
                }

            }

        }

    }
}

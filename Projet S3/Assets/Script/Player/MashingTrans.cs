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
    private bool activationTransformation;


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
        numberToAim = hitColliders.Length / 4;
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
            if (!activationTransformation)
            {
                agentTransfo.startTranformationAnim(timing);
                activationTransformation = true;
            }
            if (i.Count < hitColliders.Length / 4 && compteur > 1f)
            {
            	Physics.IgnoreLayerCollision(9, 9, false);
                Physics.IgnoreLayerCollision(9, 10, false);
                text.gameObject.SetActive(false);
                resetPlayerScript.ResetFonction(true);
            }
            else if(i.Count > hitColliders.Length / 4 && hitColliders.Length > 7)
            {
                Physics.IgnoreLayerCollision(9, 9, false);
                Physics.IgnoreLayerCollision(9, 10, false);
                text.gameObject.SetActive(false);
                agentTransfo.ActiveExplosion();
                activeExplode = true;
                //if (compteur > timing + 0.7f)
                //{
                    transform.GetComponent<PlayerMoveAlone>().enabled = true;
                //
                    StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;
                //}
            }

        }

    }
}

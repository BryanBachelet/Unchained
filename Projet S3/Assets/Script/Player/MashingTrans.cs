using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashingTrans : MonoBehaviour
{
    public int i;
    public float timing;
    private float compteur;
    public int numberToAim;

    public CamMouvement camMouvement;
    public Text text;
    
    void Start()
    {

    }

    void Update()
    {
        if (camMouvement.i >= camMouvement.cams.Count - 1)
        {
            text.enabled = true;

            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                i++;
            }
            if (compteur > timing)
            {
                if (i > numberToAim)
                {
                    Debug.Log("Win");
                    text.enabled = false;
                    StateOfGames.currentState = StateOfGames.StateOfGame.DefaultPlayable;


                }
                else
                {
                    Debug.Log("Lose");
                }
            }
            else
            {
                compteur += Time.deltaTime;
            }
        }

    }
}

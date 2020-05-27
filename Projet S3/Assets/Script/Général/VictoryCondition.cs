using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class VictoryCondition : MonoBehaviour
{
    [Header("Options")]
    public int nextSceneToLoad;
    public bool restartSameScene = true;

    [Header("Feedback")]
    public GameObject victoryText;
    private bool victoryRestart = false;
    public float timeBeforeRestart = 2f;

    public LayerMask layer;
    public bool presentation = true;
    private float compteurRestart = 0;

    private int firstFramePhase3;


    public void Update()
    {

    if(StateOfGames.currentPhase ==  StateOfGames.PhaseOfDefaultPlayable.Phase3)
    {
        if(Input.GetKeyDown(KeyCode.C) && presentation)
        {
            ReseachFormation();
            ActiveVictory();
        }
    }

        if (victoryRestart)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if (!restartSameScene)
                {
                    SceneManager.LoadScene(nextSceneToLoad);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActiveVictory();
        }
    }

    public void ActiveVictory()
    {
        if(victoryRestart==false)
        {
        Collider[] agent = Physics.OverlapSphere(Vector3.zero,300,layer);
        for(int i =0;i<agent.Length;i++)
        {
            agent[i].gameObject.SetActive(false);
        }
        victoryRestart = true;
        victoryText.SetActive(true);
        victoryText.transform.GetChild(1).gameObject.SetActive(true);
        DataPlayer.isGivingData = true;
        }
    }

    public void ReseachFormation()
    {
       if( GameObject.FindGameObjectsWithTag("Formation").Length == 0)
       {
           ActiveVictory();
       }
    }

    
}

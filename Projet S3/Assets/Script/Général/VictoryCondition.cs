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
    private float compteurRestart = 0;

        private int firstFramePhase3;

    public void Update()
    {

    if(StateOfGames.currentPhase ==  StateOfGames.PhaseOfDefaultPlayable.Phase3)
    {
      ReseachFormation();
    }

        if (victoryRestart)
        {
            if (compteurRestart > timeBeforeRestart)
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
            else
            {
                compteurRestart += Time.deltaTime;
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
       
       victoryRestart = true;
        victoryText.SetActive(true);
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

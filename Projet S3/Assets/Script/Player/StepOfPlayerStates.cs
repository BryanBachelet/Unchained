using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StepOfPlayerStates : MonoBehaviour
{
    [Header("Condition de Défaite")]
    public int[] arrayOfKill = new int[0];
    public Color[] colorFeedBack = new Color[0];
 
    public float timerBeforeLose = 5f;
    [Header("Options")]
    public bool activeLoseCondition = true;
    private float compteurBeforeLose = 0;
    private float currentStateOfTimer;
    private float maxLoseValue;
    private KillCountPlayer countPlayerKill;
    public int currentStates = 1;
    [Header("Feedback")]
    public Image sliderFill;
    public float speedOfSlider = 1f;
    private bool firstFrame;


    // Start is called before the first frame update
    void Start()
    {
        countPlayerKill = GetComponent<KillCountPlayer>();
      
        if (!activeLoseCondition)
        {
            sliderFill.gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeLoseCondition)
        {

            float currentStateKill = KillCountPlayer.killCount / arrayOfKill[currentStates];
            currentStateKill = Mathf.Clamp(currentStateKill, 0f, 1f);
            sliderFill.fillAmount = Mathf.Lerp(sliderFill.fillAmount, currentStateKill, speedOfSlider * Time.deltaTime);
            sliderFill.color = colorFeedBack[currentStates];
            if (compteurBeforeLose > timerBeforeLose)
            {
                countPlayerKill.activeDecrease = true;
            }
            else
            {
                compteurBeforeLose += Time.deltaTime;
                countPlayerKill.activeDecrease = false;
            }

        }

    }

    public void ResetTiming()
    {
        compteurBeforeLose = 0;
    }

    public void ResetDefeatCondition()
    {
        sliderFill.fillAmount = 0.2f;
        compteurBeforeLose = 0;
        currentStateOfTimer = 0.2f;
    }
}

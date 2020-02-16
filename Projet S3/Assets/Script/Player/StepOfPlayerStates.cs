using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StepOfPlayerStates : MonoBehaviour
{
    [Header("Condition de Défaite")]
    public int[] arrayOfKill = new int[0];
    [Range(0, 1f)]
    public float sizeOfCountOfKill = 0.8f;

    public float timerBeforeLose = 5f;
    [Header("Options")]
    public bool activeLoseCondition = true;
    private float compteurBeforeLose = 0;
    private float currentStateOfTimer;
    private float maxLoseValue;
    private KillCountPlayer countPlayerKill;
    private int i = 1;
    [Header("Feedback")]
    public Image sliderFill;
    public float speedOfSlider = 1f;
    private bool firstFrame;


    // Start is called before the first frame update
    void Start()
    {
        countPlayerKill = GetComponent<KillCountPlayer>();
        maxLoseValue = 1 - sizeOfCountOfKill;
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
            if (countPlayerKill.count > 0)
            {
                if (firstFrame)
                {
                    compteurBeforeLose = 0;
                    firstFrame = false;
                }

                float currentStateKill = (sizeOfCountOfKill * (KillCountPlayer.killCount.Count - arrayOfKill[i - 1]) / (arrayOfKill[i] - arrayOfKill[i - 1]));
                currentStateKill = Mathf.Clamp(currentStateKill, 0f, 1f);
                sliderFill.fillAmount = Mathf.Lerp(sliderFill.fillAmount, currentStateKill + maxLoseValue, speedOfSlider * Time.deltaTime);
            }
            else
            {
                if (!firstFrame)
                {

                    firstFrame = true;
                }
                if (compteurBeforeLose > timerBeforeLose)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    compteurBeforeLose += Time.deltaTime;
                }
                currentStateOfTimer = (1 - sizeOfCountOfKill) - ((1 - sizeOfCountOfKill) * (compteurBeforeLose / timerBeforeLose));
                sliderFill.fillAmount = Mathf.Lerp(sliderFill.fillAmount, currentStateOfTimer, speedOfSlider * Time.deltaTime);
            }
            if (KillCountPlayer.killCount.Count >= arrayOfKill[i] && i < arrayOfKill.Length - 1)
            {
                i++;

            }
        }
             
    }

    public void ResetDefeatCondition()
    {
        sliderFill.fillAmount = 0.2f;
        compteurBeforeLose = 0;
        currentStateOfTimer = 0.2f;
    }
}

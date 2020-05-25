using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashingFeedback : MonoBehaviour
{
    public int numberTransformation;
    public GameObject FirstVfxFeedback;
    public GameObject rockVfxFeedback;
    public GameObject rockVfxFeedback2;
    public GameObject feedbackSecondTransformation;

    public CinematicCam cinematicCamScript;
    private GameObject fd;
    private float duration;

    public float speedPlan1 = 50f;
    public float speedPlan2 = 100f;
    public ParticleSystem.MinMaxCurve minMaxPlan1Rock1;
    public ParticleSystem.MinMaxCurve minMaxPlan2Rock1;
    ParticleSystem.MainModule speedRock1;
    ParticleSystem.EmissionModule numberRock;
    public ParticleSystem.MinMaxCurve minMaxPlan1Rock2;
    public ParticleSystem.MinMaxCurve minMaxPlan2Rock2;
    ParticleSystem.MainModule speedRock2;

    public AnimationCurve animCurveRockNumber;
    float tempsEcouleGainRock;
    public ParticleSystem.MinMaxCurve rockNumberCurve;
    public void Start()
    {
        tempsEcouleGainRock = 0;
        cinematicCamScript = Camera.main.GetComponent<CinematicCam>();
    }

    void Update()
    {
        if (fd != null)
        {
            fd.transform.position = transform.position;
            if (rockVfxFeedback != null)
            {
                if(cinematicCamScript.currentPlan == CinematicCam.NamePlan.Plan1)
                {
                    //speed.startSpeedMultiplier = speedPlan1;
                    speedRock1.startSpeed = minMaxPlan1Rock1;
                    numberRock.rateOverTime = rockNumberCurve;
                    speedRock2.startSpeed = minMaxPlan1Rock2;
                }
                if (cinematicCamScript.currentPlan == CinematicCam.NamePlan.Plan2)
                {
                    //speed.startSpeedMultiplier = speedPlan2;
                    speedRock1.startSpeed = minMaxPlan2Rock1;
                    speedRock2.startSpeed = minMaxPlan2Rock2;
                }

            }
        }

    }


    public void ActiveFeedback()
    {
        bool activeTransformation = false;
        GetDuration();
        if (!activeTransformation)
        {
            numberTransformation++;
            if (numberTransformation == 1)
            {

                fd = Instantiate(FirstVfxFeedback, transform.position + Vector3.up, Quaternion.Euler(0, 0, 0));
                rockVfxFeedback = fd.transform.GetChild(0).transform.GetChild(4).gameObject;
                speedRock1 = rockVfxFeedback.GetComponent<ParticleSystem>().main;
                numberRock = rockVfxFeedback.GetComponent<ParticleSystem>().emission;
                rockVfxFeedback2 = fd.transform.GetChild(0).transform.GetChild(13).gameObject;
                speedRock2 = rockVfxFeedback2.GetComponent<ParticleSystem>().main;
                fd.GetComponent<MagicalFX.FX_LifeTime>().LifeTime = duration;
            }
            if (numberTransformation == 2)
            {
                fd = Instantiate(feedbackSecondTransformation, transform.position, Quaternion.Euler(-90, 0, 0));
                fd.GetComponent<MagicalFX.FX_LifeTime>().LifeTime = duration;
            }
            activeTransformation = true;
        }

    }
    public void GetDuration()
    {
        duration = Camera.main.GetComponent<CinematicCam>().durationTotal;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string trackTest1;
    public FMOD.Studio.EventInstance track1;
    public TransformationPlayerStates myTfP;

    public static bool checkP1;
    public static bool checkP2;
    public float tempsAvantMusic;
    float tempsEcouleMusic;
    public bool isCheckMusic = false;
    // Start is called before the first frame update
    void Start()
    {
        isCheckMusic = false;
        checkP1 = false;
        checkP2 = false;
        tempsEcouleMusic = 0;
        track1 = FMODUnity.RuntimeManager.CreateInstance(trackTest1);
        track1.setParameterByName("TransiP1", 0F);
        track1.setParameterByName("TransiP2", 0F);
    }

    // Update is called once per frame
    void Update()
    {
        if (tempsEcouleMusic < tempsAvantMusic)
        {
            tempsEcouleMusic += Time.deltaTime;
        }
        else
        {
            if (!isCheckMusic)
            {
                track1.start();
                isCheckMusic = true;
            }

        }
        if (checkP1)
        {
            track1.setParameterByName("TransiP1", 1F);
        }
        if (checkP2)
        {
            track1.setParameterByName("TransiP2", 1F);
        }
    }
}

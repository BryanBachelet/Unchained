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
    // Start is called before the first frame update
    void Start()
    {
        track1 = FMODUnity.RuntimeManager.CreateInstance(trackTest1);
        track1.setParameterByName("TransiP1", 0F);
        track1.setParameterByName("TransiP2", 0F);
    }

    // Update is called once per frame
    void Update()
    {
        if( checkP1)
        {
            track1.setParameterByName("TransiP1", 1F);
        }
        if(checkP2)
        {
            track1.setParameterByName("TransiP2", 1F);
        }
    }
}

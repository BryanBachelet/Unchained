using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMusicTest : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string trackTest10;
    public FMOD.Studio.EventInstance track10;

    public bool PlayInput = false;
    public bool stopInput = false;
    // Start is called before the first frame update
    void Start()
    {
        track10 = FMODUnity.RuntimeManager.CreateInstance(trackTest10);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayInput)
        {
            PlayInput = false;
            track10.start();
        }
        if(stopInput)
        {
            stopInput = false;
            track10.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}

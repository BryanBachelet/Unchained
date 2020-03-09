using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string trackTest1;
    public FMOD.Studio.EventInstance track1;
    // Start is called before the first frame update
    void Start()
    {
        track1 = FMODUnity.RuntimeManager.CreateInstance(trackTest1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

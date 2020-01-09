using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
public class MusicManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float musicValue;

    [FMODUnity.EventRef]
    public string eventName;
    public FMOD.Studio.EventInstance fmodEvent;
    //public FMOD.Studio.ParameterInstance parameterEvent;
    // Start is called before the first frame update
    void Start()
    {
        fmodEvent = FMODUnity.RuntimeManager.CreateInstance(eventName);
        fmodEvent.getParameterByName("StateMusic", out musicValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

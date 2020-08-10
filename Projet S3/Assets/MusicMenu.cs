using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string trackMenu;
    public FMOD.Studio.EventInstance soundMenu;

    // Start is called before the first frame update
    void Start()
    {
        soundMenu = FMODUnity.RuntimeManager.CreateInstance(trackMenu);
        soundMenu.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

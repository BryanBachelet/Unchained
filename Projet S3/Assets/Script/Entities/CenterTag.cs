using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTag : MonoBehaviour
{
    public enum Types { Blue, Orange, Violet }
    public Types centerTypes;
    public GameObject[] centerVFX;
    public bool finVfxSeries = false;

    //[FMODUnity.EventRef]
    public string incantation;
    private FMOD.Studio.EventInstance incantationSound;
    public bool isInvoking;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(centerVFX[3], transform.position, transform.rotation);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public AnimationCurve howItPulse;
    float tempsEcoulePulse;
    public float maxKey;
    Material myMat;
    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            tempsEcoulePulse = 0;
        }
        if(tempsEcoulePulse < maxKey)
        {
            tempsEcoulePulse += Time.deltaTime;
        }
        myMat.SetFloat("_MaskMultiplier", howItPulse.Evaluate(tempsEcoulePulse));
    }
}

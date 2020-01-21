using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSet : MonoBehaviour
{
    public Material matToChange;
    public float maxOffSet;
    float currentOffSet = 1;
    bool bla = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(bla)
            {
                bla = false;
            }
            else
            {
                bla = true;
            }
        }
        if(bla)
        {
            currentOffSet -= Time.deltaTime / 2;
        }
        else if (!bla)
        {
            currentOffSet += Time.deltaTime / 2;
        }
        //matToChange.SetTextureScale("_MainTex", new Vector2(0, currentOffSet));
        matToChange.SetTextureOffset("_MainTex", new Vector2(0, currentOffSet));

    }
}

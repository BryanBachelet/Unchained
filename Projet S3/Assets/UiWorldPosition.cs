using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiWorldPosition : MonoBehaviour
{
    public Vector3 mypos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mypos = transform.position;
        //mypos = RectTransformUtility.WorldToScreenPoint(Camera.main, mypos);
        //mypos = RectTransformUtility.ScreenPointToWorldPointInRectangle(gameObject.GetComponent<RectTransform>(), new Vector2(mypos.x, mypos.y), Camera.main, out m);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColor : MonoBehaviour
{
    public LineRenderer line;
    public Color colorShoot;
    public Color colorRotate;
    public float speedOfChangeColor = 1;

    private Color currentColor;
    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInParent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(StateAnim.state != StateAnim.CurrentState.Rotate)
        {
            ChangeColor(colorShoot);

        }
        else
        {
            ChangeColor(colorRotate);
        }
        line.material.SetColor("_EmissionColor", currentColor);
    }

   public void ChangeColor(Color colorTaget)
    {
        if (currentColor != colorTaget)
        {
            currentColor = Color.Lerp(currentColor, colorTaget, t);
            t = speedOfChangeColor * Time.deltaTime;
        }
        else
        {
            t = 0;
        }
    }
}

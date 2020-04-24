using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MashingTest : MonoBehaviour
{
    public List<float> inputPerSeconde;
    public Text text;
    public float moyenne;
    private int i;

    public float tempsForResetLoop;
    float tempsEcouleLoop;
    public List<int> inputPerMidLoop;
    public List<float> allInputTake;
    // Start is called before the first frame update
    void Start()
    {
        tempsEcouleLoop = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Add();

        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            moyenne = 0;
          
            i++;
        }

        tempsEcouleLoop += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse3) || Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            allInputTake.Add(Time.time);
        }
        if(tempsEcouleLoop > 20)
        {
            inputPerMidLoop.Add(allInputTake.Count);
            allInputTake.Clear();
            tempsEcouleLoop = 0;
        }
        //else if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse3))
        //{
        //    allInputTake.Add(Time.time);
        //}
        //


        for (int i = 0; i < inputPerSeconde.Count; i++)
        {
            inputPerSeconde[i] -= Time.deltaTime;
            if(inputPerSeconde[i] < 0)
            {
                inputPerSeconde.RemoveAt(i);
            }
        }
        if(text != null)
            text.text = inputPerSeconde.Count.ToString();
    }

   public void Add()
    {
        inputPerSeconde.Add(1);
        i++;
    } 
}

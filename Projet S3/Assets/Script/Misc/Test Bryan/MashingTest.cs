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
    // Start is called before the first frame update
    void Start()
    {
        
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



        for (int i = 0; i < inputPerSeconde.Count; i++)
        {
            inputPerSeconde[i] -= Time.deltaTime;
            if(inputPerSeconde[i] < 0)
            {
                inputPerSeconde.RemoveAt(i);
            }
        }
        text.text = inputPerSeconde.Count.ToString();
    }

   public void Add()
    {
        inputPerSeconde.Add(1);
        i++;
    } 
}

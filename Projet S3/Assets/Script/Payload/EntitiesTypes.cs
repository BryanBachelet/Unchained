using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesTypes : MonoBehaviour
{
    public enum Types {Blue,Orange,Violet }
    public Types entitiesTypes;

    private void Start()
    {
        if (entitiesTypes == Types.Blue)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (entitiesTypes == Types.Orange)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (entitiesTypes == Types.Violet)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;
    private void OnMouseEnter()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        Cursor.visible = true;

    }
    void Update()
    {


    }
}

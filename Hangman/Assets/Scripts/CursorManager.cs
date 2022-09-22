using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorNormal;
    public Vector2 normalCursorHotspot;
    public Texture2D cursorOnButton;
    public Vector2 onButtonCursorHotspot;

    public void OnButtonCursorEnter()
    {
        Cursor.SetCursor(cursorOnButton, onButtonCursorHotspot, CursorMode.Auto);
    }

    public void OnButtonCursorExit()
    {
        Cursor.SetCursor(cursorNormal, normalCursorHotspot, CursorMode.Auto);
    }
}

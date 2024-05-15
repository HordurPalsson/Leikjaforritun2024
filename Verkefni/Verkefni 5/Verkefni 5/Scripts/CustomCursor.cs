using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private Texture2D cursorTexture;
    private Vector3 cursorPos;
    public Transform cursorSprite;

    private void Start ()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void Update ()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorSprite.position = new Vector3(cursorPos.x, cursorPos.y, 0);
    }
}

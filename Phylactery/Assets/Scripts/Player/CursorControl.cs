using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    [SerializeField]
    private Sprite _cursorTexture;

    private Vector2 _cursorPos;

    // Start is called before the first frame update
    void Start()
    {
        _cursorPos = new Vector2(_cursorTexture.texture.width / 2, _cursorTexture.texture.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime > 0)
        {
            Cursor.SetCursor(_cursorTexture.texture, _cursorPos, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}

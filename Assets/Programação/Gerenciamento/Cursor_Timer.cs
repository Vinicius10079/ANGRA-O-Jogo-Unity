using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Timer : MonoBehaviour
{
    public float duraçãoDeVisibilidadeDoCursor = 1.2f;

    bool cursorVisivel;

    Coroutine co_OcultarCursor;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") == 0 && (Input.GetAxis("Mouse Y") == 0))
        {
            if (co_OcultarCursor == null)
            {
                co_OcultarCursor = StartCoroutine(HideCursor());
            }
        }
        else
        {
            if (co_OcultarCursor != null)
            {
                StopCoroutine(co_OcultarCursor);
                co_OcultarCursor = null;
                cursorVisivel = true;
            }
        }

        if (cursorVisivel)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    private IEnumerator HideCursor()
    {
        yield return new WaitForSeconds(duraçãoDeVisibilidadeDoCursor);

        cursorVisivel = false;
        
    }
}

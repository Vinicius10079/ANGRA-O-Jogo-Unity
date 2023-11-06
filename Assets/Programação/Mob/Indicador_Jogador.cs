using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicador_Jogador : MonoBehaviour
{
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (transform.parent.eulerAngles.y == 180)
        {
            sr.flipX = true;
        }
        if (transform.parent.eulerAngles.y == 0)
        {
            sr.flipX = false;
        }
    }

    public void DesabilitarAnimador()
    {
        GetComponent<Animator>().enabled = false;
    }
}

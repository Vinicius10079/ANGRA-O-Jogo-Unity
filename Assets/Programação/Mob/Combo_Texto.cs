using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo_Texto : MonoBehaviour
{
    public int combo;
    public float dura��o = 5f;
    public string[] nomes;

    bool contagem;

    Pontua��o pts;
    Animator anim;
    Text texto;

    void Awake()
    {
        anim = GetComponent<Animator>();
        texto = GetComponent<Text>();
        pts = FindObjectOfType<Pontua��o>();
    }

    void Update()
    {
        if (dura��o < 0)
        {
            pts.pontua��o += 100 * combo;
            combo = 0;
            anim.enabled = false;
            texto.text = "";
            dura��o = 0;
        }
        if (combo > 1 && anim.enabled == true)
        {
            dura��o -= Time.deltaTime;
        }
        if (anim.enabled == false)
        {
            texto.text = "";
        }

        if (combo > 1 && combo < 4 && texto.text != nomes[0])
        {
            dura��o = 1f;
            texto.text = nomes[0];
            anim.enabled = true;
            anim.Play(nomes[0]);
        }
        if (combo > 3 && combo < 6 && texto.text != nomes[1])
        {
            dura��o = 1f;
            texto.text = nomes[1];
            anim.Play(nomes[1]);
        }
        if (combo > 5 && combo < 8 && texto.text != nomes[2])
        {
            dura��o = 1f;
            texto.text = nomes[2];
            anim.Play(nomes[2]);
        }
        if (combo > 7 && combo < 12 && texto.text != nomes[3])
        {
            dura��o = 1f;
            texto.text = nomes[3];
            anim.Play(nomes[3]);
        }
        if (combo > 11 && texto.text != nomes[4])
        {
            dura��o = 1f;
            texto.text = nomes[4];
            anim.Play(nomes[4]);
        }
    }
}

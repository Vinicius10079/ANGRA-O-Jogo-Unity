using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo_Texto : MonoBehaviour
{
    public int combo;
    public float duração = 5f;
    public string[] nomes;

    bool contagem;

    Pontuação pts;
    Animator anim;
    Text texto;

    void Awake()
    {
        anim = GetComponent<Animator>();
        texto = GetComponent<Text>();
        pts = FindObjectOfType<Pontuação>();
    }

    void Update()
    {
        if (duração < 0)
        {
            pts.pontuação += 100 * combo;
            combo = 0;
            anim.enabled = false;
            texto.text = "";
            duração = 0;
        }
        if (combo > 1 && anim.enabled == true)
        {
            duração -= Time.deltaTime;
        }
        if (anim.enabled == false)
        {
            texto.text = "";
        }

        if (combo > 1 && combo < 4 && texto.text != nomes[0])
        {
            duração = 1f;
            texto.text = nomes[0];
            anim.enabled = true;
            anim.Play(nomes[0]);
        }
        if (combo > 3 && combo < 6 && texto.text != nomes[1])
        {
            duração = 1f;
            texto.text = nomes[1];
            anim.Play(nomes[1]);
        }
        if (combo > 5 && combo < 8 && texto.text != nomes[2])
        {
            duração = 1f;
            texto.text = nomes[2];
            anim.Play(nomes[2]);
        }
        if (combo > 7 && combo < 12 && texto.text != nomes[3])
        {
            duração = 1f;
            texto.text = nomes[3];
            anim.Play(nomes[3]);
        }
        if (combo > 11 && texto.text != nomes[4])
        {
            duração = 1f;
            texto.text = nomes[4];
            anim.Play(nomes[4]);
        }
    }
}

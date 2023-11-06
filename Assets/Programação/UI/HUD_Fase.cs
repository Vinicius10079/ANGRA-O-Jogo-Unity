using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Fase : MonoBehaviour
{
    public Chefe chefe;
    public int jogador_ID;
    public Mob jogador;
    public Image ícone;
    public Image preenchimentoDeBarra_Energia;
    public Image preenchimentoDeBarra_Munição;

    Mob mob_chefe;

    void Awake()
    {
        if (chefe == null)
        {
            switch (jogador_ID)
            {
                case 1:
                    jogador = FindObjectOfType<GerenciadorDeFase>().jogador1.GetComponent<Mob>();
                    break;

                case 2:
                    jogador = FindObjectOfType<GerenciadorDeFase>().jogador2.GetComponent<Mob>();
                    break;
            }
        }
        else
        {
            mob_chefe = chefe.GetComponent<Mob>();
        }
    }

    void Update()
    {
        if (chefe == null)
        {
            preenchimentoDeBarra_Energia.fillAmount = jogador.energia.x / jogador.energia.y;

            if (preenchimentoDeBarra_Munição != null)
            {
                preenchimentoDeBarra_Munição.fillAmount = jogador.munição.x / jogador.munição.y;
            }
        }
        else
        {
            preenchimentoDeBarra_Energia.fillAmount = mob_chefe.energia.x / mob_chefe.energia.y;
        }

    }
}

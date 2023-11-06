using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamentosBásicos : MonoBehaviour
{
    public bool desafiliar;
    public bool originalizarNome_Prefabs;
    public bool destuirPorTempo;
    public float tempoAtéDestruição;
    public bool destruir_Público;
    public bool desativar_Público;
    public bool destuirAoSeTornarInvisível;
    public bool tocarSom_Público;
    public bool prefabDeSom;

    void Awake()
    {
        if (desafiliar)
        {
            transform.parent = null;
        }
    }

    void OnBecameInvisible()
    {
        if (destuirAoSeTornarInvisível)
        {
            AutoDestruir();
        }
    }

    void Update()
    {
        if (destuirPorTempo)
        {
            if (tempoAtéDestruição > 0)
            {
                tempoAtéDestruição -= Time.deltaTime;
            }
            else if (tempoAtéDestruição <= 0)
            {
                tempoAtéDestruição = 0;
                AutoDestruir();
            }
        }

        if (prefabDeSom == true && GetComponent<AudioSource>().isPlaying == false)
        {
            AutoDestruir();
        }
    }

    public void AutoDesativar_Público()
    {
        if (desativar_Público)
        {
            AutoDesativar();
        }
    }

    public void AutoDestruir_Público()
    {
        if (destruir_Público)
        {
            AutoDestruir();
        }
    }

    public void TocarSom()
    {
        if (tocarSom_Público)
        {
            GetComponent<AudioSource>().enabled = true;
        }
    }

    void AutoDesativar()
    {
        gameObject.SetActive(false);
    }

    void AutoDestruir()
    {
        Destroy(gameObject);
    }
}

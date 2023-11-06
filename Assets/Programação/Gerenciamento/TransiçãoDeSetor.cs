using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransiçãoDeSetor : MonoBehaviour
{
    public GameObject setorÀAtivar;
    public GameObject setorÀDesativar;
    public GameObject telaPreta;
    public float delayAtéTransição = 1f;

    public Transform pontoDeAparição1;
    public Transform pontoDeAparição2;
    public Transform âncora_CâmeraDoPróximoSetor;

    bool entrouEmColisão, corrotinaIniciada_Transicionar;
    GameObject objetoColidido;
    Setor setor;

    void Awake()
    {
        setor = GetComponent<Setor>();
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Inimigo").Length == 0 && corrotinaIniciada_Transicionar == false)
        {
            StartCoroutine(Transicionar());
        }
    }

    IEnumerator Transicionar()
    {
        corrotinaIniciada_Transicionar = true;

        yield return new WaitForSeconds(delayAtéTransição);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out (Semi-Prolongado)");

        yield return new WaitForSeconds(1f);

        setorÀAtivar.SetActive(true);

        foreach (GameObject jogadorJgds in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (jogadorJgds.GetComponent<Transição_Jogador>().jogador == 1)
            {
                jogadorJgds.transform.position = pontoDeAparição1.position;
            }
            if (jogadorJgds.GetComponent<Transição_Jogador>().jogador == 2)
            {
                jogadorJgds.transform.position = pontoDeAparição2.position;
            }
        }

        PlayerPrefs.SetInt("checado", PlayerPrefs.GetInt("checado") + 1);

        Invoke("TranspôrCâmera", 0.1f);
    }

    void TranspôrCâmera()
    {
        FindObjectOfType<Camera>().transform.position = âncora_CâmeraDoPróximoSetor.position;
        FindObjectOfType<FunçõesDeCâmera>().AtualizarPosiçãoInicial();

        setorÀDesativar.SetActive(false);
    }
}

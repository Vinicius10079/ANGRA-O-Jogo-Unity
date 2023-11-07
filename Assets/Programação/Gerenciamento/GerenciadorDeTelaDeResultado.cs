using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerenciadorDeTelaDeResultado : MonoBehaviour
{
    public GameObject telaPreta;
    public GameObject continuar_Texto;
    public GameObject confirma��o_sfx;
    public Text textoDePontua��o;
    public Text textoDeMelhorPontua��o;

    bool podeContinuar;
    bool mudarCena_Chamado;

    int pontua��oAtual;
    int pontua��o1;
    bool salvo = false;

    void Awake()
    {
        telaPreta.SetActive(true);

        pontua��oAtual = PlayerPrefs.GetInt("Pontuacao");
        pontua��o1 = PlayerPrefs.GetInt("Pontuacao 1");
        textoDePontua��o.text = "SUA PONTUA��O: " + pontua��oAtual;
        if (pontua��oAtual > pontua��o1)
        {
            textoDeMelhorPontua��o.text = "MELHOR PONTUA��O: " + pontua��oAtual;
            PlayerPrefs.SetInt("Pontuacao 1", pontua��oAtual);
        }
        else
        {
            textoDeMelhorPontua��o.text = "MELHOR PONTUA��O: " + pontua��o1;
        }
        
    }

    void Start()
    {
        Invoke("PodeContinuar", 3f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Start") && mudarCena_Chamado == false && podeContinuar)
        {
            if (confirma��o_sfx != null)
            {
                Instantiate(confirma��o_sfx);
            }

            StartCoroutine(MudarDeCena("Abertura"));

            //Instantiate(somDeConfirma��o_sfx);
        }
    }

    void PodeContinuar()
    {
        continuar_Texto.SetActive(true);
        podeContinuar = true;
    }

    public IEnumerator MudarDeCena(string cena)
    {
        mudarCena_Chamado = true;

        print("Mudar para Cena: " + cena);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(cena);
    }
}

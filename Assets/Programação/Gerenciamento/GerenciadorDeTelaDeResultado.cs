using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerenciadorDeTelaDeResultado : MonoBehaviour
{
    public GameObject telaPreta;
    public GameObject continuar_Texto;
    public GameObject confirmação_sfx;
    public Text textoDePontuação;
    public Text textoDeMelhorPontuação;

    bool podeContinuar;
    bool mudarCena_Chamado;

    int pontuaçãoAtual;
    int pontuação1;
    bool salvo = false;

    void Awake()
    {
        telaPreta.SetActive(true);

        pontuaçãoAtual = PlayerPrefs.GetInt("Pontuacao");
        pontuação1 = PlayerPrefs.GetInt("Pontuacao 1");
        textoDePontuação.text = "SUA PONTUAÇÃO: " + pontuaçãoAtual;
        if (pontuaçãoAtual > pontuação1)
        {
            textoDeMelhorPontuação.text = "MELHOR PONTUAÇÃO: " + pontuaçãoAtual;
            PlayerPrefs.SetInt("Pontuacao 1", pontuaçãoAtual);
        }
        else
        {
            textoDeMelhorPontuação.text = "MELHOR PONTUAÇÃO: " + pontuação1;
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
            if (confirmação_sfx != null)
            {
                Instantiate(confirmação_sfx);
            }

            StartCoroutine(MudarDeCena("Abertura"));

            //Instantiate(somDeConfirmação_sfx);
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

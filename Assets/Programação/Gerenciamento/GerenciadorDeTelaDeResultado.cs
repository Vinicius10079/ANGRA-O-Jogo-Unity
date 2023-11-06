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

    bool podeContinuar;
    bool mudarCena_Chamado;

    int pontuaçãoAtual;
    int pontuação1;
    bool salvo = false;
    string nome;

    void Awake()
    {
        telaPreta.SetActive(true);

        pontuaçãoAtual = PlayerPrefs.GetInt("Pontuacao");
        pontuação1 = PlayerPrefs.GetInt("Pontuacao 1");
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
            SalvarPontuação();
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

    public void SalvarPontuação()
    {
        if (nome == "")
        {
            nome = "PLR";
        }

        if (pontuaçãoAtual >= pontuação1 && salvo == false)
        {
            PlayerPrefs.SetInt("Pontuacao 1", pontuaçãoAtual);
            PlayerPrefs.SetString("Nome 1", nome);
            salvo = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerenciadorDeTelaDeTítulo : MonoBehaviour
{
    public GameObject telaPreta;
    public GameObject lampejo;
    public GameObject iniciar_Texto;
    public string cenaACarregar;
    public GameObject confirmação_sfx;

    bool podeIniciar;
    bool mudarCena_Chamado;

    AsyncOperation operacao;

    void Awake()
    {
        Time.timeScale = 1;
        telaPreta.SetActive(true);

        PlayerPrefs.DeleteKey("checado");
    }

    void Start()
    {
        Invoke("PodeIniciar", 1f);

        operacao = SceneManager.LoadSceneAsync("Introducao");
        operacao.allowSceneActivation = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Start") 
            && mudarCena_Chamado == false && podeIniciar)
        {
            if (confirmação_sfx != null)
            {
                Instantiate(confirmação_sfx);
            }
            
            iniciar_Texto.GetComponent<Animator>().enabled = false;
            iniciar_Texto.GetComponent<Text>().enabled = true;

            GetComponent<AudioSource>().enabled = false;

            StartCoroutine(MudarDeCena(cenaACarregar));

            //Instantiate(somDeConfirmação_sfx);
        }
    }

    void PodeIniciar()
    {
        iniciar_Texto.SetActive(true);
        podeIniciar = true;
    }

    public IEnumerator MudarDeCena(string cena)
    {
        mudarCena_Chamado = true;

        print("Mudar para Cena: " + cena);

        yield return new WaitForSeconds(0.5f);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(cena);
    }
}

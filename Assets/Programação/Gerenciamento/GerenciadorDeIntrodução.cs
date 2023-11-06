using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDeIntrodução : MonoBehaviour
{
    public GameObject telaPreta;
    public float duração = 4f;
    public Text texto;
    public string cenaParaCarregar = "Fase";

    public string[] frases;

    AsyncOperation operação;

    void Start()
    {
        operação = SceneManager.LoadSceneAsync(cenaParaCarregar);
        operação.allowSceneActivation = false;

        StartCoroutine(MudarDeCena_Corrotina());

        texto.text = frases[Random.Range(0, frases.Length)];
    }

    IEnumerator MudarDeCena_Corrotina()
    {
        yield return new WaitForSeconds(duração);


        yield return new WaitForSecondsRealtime(0.6f);

        SceneManager.LoadScene(cenaParaCarregar);
    }
}

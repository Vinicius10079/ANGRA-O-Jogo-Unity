using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerenciadorDeEncerramento : MonoBehaviour
{
    public GameObject telaPreta;
    public float duração = 6f;
    public Text texto;

    public string[] frases;

    void Start()
    {
        StartCoroutine(MudarDeCena_Corrotina());

        texto.text = frases[Random.Range(0, frases.Length)];
    }

    IEnumerator MudarDeCena_Corrotina()
    {
        yield return new WaitForSeconds(duração);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out (Prolongado)");

        yield return new WaitForSecondsRealtime(3f);

        SceneManager.LoadScene("Tela de Resultados");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDeGameOver : MonoBehaviour
{
    public float atraso;
    public GameObject telaPreta;

    void Awake()
    {
        PlayerPrefs.DeleteKey("checado");
    }

    void Start()
    {
        StartCoroutine(MudarDeCena());
    }

    IEnumerator MudarDeCena()
    {
        print("Mudar para Cena: Tela de Título");

        yield return new WaitForSeconds(atraso);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Tela de Título");
    }
}

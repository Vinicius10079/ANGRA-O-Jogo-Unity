using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDeAbertura : MonoBehaviour
{
    public GameObject telaPreta;


    void Start()
    {
        StartCoroutine(MudarDeCena());
    }

    IEnumerator MudarDeCena()
    {
        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade In");

        yield return new WaitForSeconds(2f);

        telaPreta.GetComponent<Animator>().Play("Fade Out");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Homenagem");
    }
}

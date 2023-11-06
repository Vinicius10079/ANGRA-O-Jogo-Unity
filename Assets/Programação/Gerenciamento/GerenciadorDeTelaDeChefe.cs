using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GerenciadorDeTelaDeChefe : MonoBehaviour
{
    public Sprite[] artesDeChefe;
    public Color[] coresDeChefe;
    public string[] nomesDeChefe;

    public GameObject lampejo, telaPreta;

    void Awake()
    {
        switch (PlayerPrefs.GetString("Chefe a Apresentar"))
        {
            case "Cut Man":
                
                break;

            case "Elec Man":
                
                break;

            case "Ice Man":
                
                break;

            case "Fire Man":
                
                break;

            case "Bomb Man":
                
                break;

            case "Guts Man":
                
                break;

            case "Time Man":
                
                break;

            case "Oil Man":
                
                break;
        }
    }

    void Start()
    {
        StartCoroutine(MudarDeCena("Fase de " + PlayerPrefs.GetString("Chefe a Apresentar")));
    }

    public IEnumerator MudarDeCena(string cena)
    {
        print("Mudar para Cena: " + cena);

        yield return new WaitForSeconds(7f);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out");

        yield return new WaitForSeconds(1f);

        if (cena != "")
        {
            SceneManager.LoadScene(cena);
        }
        else
        {
            SceneManager.LoadScene("Fase de Cut Man");
        }
        
    }
}

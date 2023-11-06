using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GerenciadorDeTelaDePoder : MonoBehaviour
{
    public string poder;
    public bool podeAvançar;
    public string cenaDeSeleçãoDeEstágio_Nome;

    public GameObject telaPreta;

    bool corrotinaIniciada;

    Image rend1, rend2;
    Animator anim;

    void Awake()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        rend1 = GameObject.Find("Renderização (Jogador 1)").GetComponent<Image>();
        rend2 = GameObject.Find("Renderização (Jogador 2)").GetComponent<Image>();

        if (poder == null || poder == "")
        {
            poder = "cutman";
        }

        switch (PlayerPrefs.GetInt("Animação de Tela de Poder"))
        {
            case 1:
                anim.Play("Tela de Poder 1");
                break;

            case 2:
                GameObject.Find("Título").GetComponent<TextMeshProUGUI>().text = "Vocês Ganharam";
                anim.Play("Tela de Poder 2");
                break;

            case 3:
                anim.Play("Tela de Poder 3");
                break;
        }
    }

    void Start()
    {
        Invoke("PodeAvançar", 4f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && podeAvançar && corrotinaIniciada == false)
        {
            SalvarProgresso();
            StartCoroutine(MudarDeCena());
        }
    }

    void PodeAvançar()
    {
        podeAvançar = true;
    }

    void SalvarProgresso()
    {
        switch (PlayerPrefs.GetString("Ganhar Poder"))
        {
            case "Rolling Cutter":
                PlayerPrefs.SetInt("Cut Man derrotado", 1);
                break;

            case "Thunder Beam":
                PlayerPrefs.SetInt("Elec Man derrotado", 1);
                break;

            case "Ice Slasher":
                PlayerPrefs.SetInt("Ice Man derrotado", 1);
                break;

            case "Fire Storm":
                PlayerPrefs.SetInt("Fire Man derrotado", 1);
                break;

            case "Hyper Bomb":
                PlayerPrefs.SetInt("Bomb Man derrotado", 1);
                break;

            case "Super Arm":
                PlayerPrefs.SetInt("Guts Man derrotado", 1);
                break;

            case "Time Slower":
                PlayerPrefs.SetInt("Time Man derrotado", 1);
                break;

            case "Oil Slider":
                PlayerPrefs.SetInt("Oil Man derrotado", 1);
                break;
        }
    }

    public IEnumerator MudarDeCena()
    {
        corrotinaIniciada = true;
        print("Mudar para Cena: " + cenaDeSeleçãoDeEstágio_Nome);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(cenaDeSeleçãoDeEstágio_Nome);
    }
}

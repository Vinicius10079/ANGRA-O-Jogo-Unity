using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GerenciadorDeTelaDePoder : MonoBehaviour
{
    public string poder;
    public bool podeAvan�ar;
    public string cenaDeSele��oDeEst�gio_Nome;

    public GameObject telaPreta;

    bool corrotinaIniciada;

    Image rend1, rend2;
    Animator anim;

    void Awake()
    {
        anim = GameObject.Find("Canvas").GetComponent<Animator>();
        rend1 = GameObject.Find("Renderiza��o (Jogador 1)").GetComponent<Image>();
        rend2 = GameObject.Find("Renderiza��o (Jogador 2)").GetComponent<Image>();

        if (poder == null || poder == "")
        {
            poder = "cutman";
        }

        switch (PlayerPrefs.GetInt("Anima��o de Tela de Poder"))
        {
            case 1:
                anim.Play("Tela de Poder 1");
                break;

            case 2:
                GameObject.Find("T�tulo").GetComponent<TextMeshProUGUI>().text = "Voc�s Ganharam";
                anim.Play("Tela de Poder 2");
                break;

            case 3:
                anim.Play("Tela de Poder 3");
                break;
        }
    }

    void Start()
    {
        Invoke("PodeAvan�ar", 4f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && podeAvan�ar && corrotinaIniciada == false)
        {
            SalvarProgresso();
            StartCoroutine(MudarDeCena());
        }
    }

    void PodeAvan�ar()
    {
        podeAvan�ar = true;
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
        print("Mudar para Cena: " + cenaDeSele��oDeEst�gio_Nome);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(cenaDeSele��oDeEst�gio_Nome);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDeFase : MonoBehaviour
{
    public string chefe;
    public GameObject chefe_Objeto;
    public int checada;
    public bool podePausar = false;

    [Header("Jogador 1")]
    public string esperaJ1;
    public float tempoDeEsperaJ1;
    public GameObject jogador1;
    //public GameObject jogador1Ativo;
    public GameObject hudJ1;
    public Text esperaJ1_Texto;

    [Header("Jogador 2")]
    public string esperaJ2;
    public float tempoDeEsperaJ2;
    public GameObject jogador2;
    //public GameObject jogador2Ativo;
    public GameObject hudJ2;
    public Text esperaJ2_Texto;

    [Header("Músicas de Fundo")]
    public AudioClip[] músicas;

    [Header("Outros")]
    public int vidasExtras_I;
    public int combo;
    public GameObject telaPreta;
    public bool concluída;
    public float escalaTemporal = 1;
    public GameObject menuDePausa;
    public string cenaÀCarregar;
    public GameObject[] setores;
    public Transform[] âcorasDeCâmera;
    public GameObject menuDeMorte;
    public AudioClip jingleDeVitória;

    public bool pause;
    
    float tempoDeEsperaJ1_I, tempoDeEsperaJ2_I;

    Camera cam;

    void Awake()
    {
        escalaTemporal = 1;
        cam = FindObjectOfType<Camera>();

        foreach (GameObject setor in setores)
        {
            setor.SetActive(false);
        }

        setores[PlayerPrefs.GetInt("checado")].SetActive(true);
        cam.transform.position = âcorasDeCâmera[PlayerPrefs.GetInt("checado")].position;

        tempoDeEsperaJ1_I = tempoDeEsperaJ1;
        tempoDeEsperaJ2_I = tempoDeEsperaJ2;

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade In");

        jogador1.SetActive(true);
    }

    void Start()
    {
        GetComponent<AudioSource>().clip = músicas[Random.Range(0, músicas.Length)];
        GetComponent<AudioSource>().enabled = true;
    }

    void Update()
    {
        //IrParaOChefe();

        if (esperaJ1 == "iniciar")
        {
            tempoDeEsperaJ1 = tempoDeEsperaJ1_I;
            esperaJ1 = "contagem";
        }
        else if (esperaJ1 == "contagem")
        {
            tempoDeEsperaJ1 -= Time.deltaTime;

            if (tempoDeEsperaJ1 <= 0)
            {
                esperaJ1 = "pronto";
            }
        }

        if (esperaJ2 == "iniciar")
        {
            tempoDeEsperaJ2 = tempoDeEsperaJ2_I;
            esperaJ2 = "contagem";
        }
        else if (esperaJ2 == "contagem")
        {
            tempoDeEsperaJ2 -= Time.deltaTime;

            //j1_hud.espera_Texto.enabled = false;

            if (tempoDeEsperaJ2 <= 0)
            {
                tempoDeEsperaJ2 = 0;
                esperaJ2 = "pronto";
            }
        }

        AtualizarTextosDeEspera();

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Start"))
        {
            if (esperaJ1 == "pronto")
            {
                esperaJ1 = "";
                esperaJ1_Texto.gameObject.SetActive(false);
                hudJ1.SetActive(true);

                jogador1.transform.position = GameObject.Find("Ponto Inicial (J1)").transform.position;
                jogador1.SetActive(true);
                //jogador1.GetComponent<Jogador1>().Invoke("DesativarInvencibilidade", 2f);
            }
        }
        if (Input.GetKeyDown(KeyCode.M) /*|| Input.GetButtonDown("Start 2")*/)
        {
            if (esperaJ2 == "pronto")
            {
                esperaJ2 = "";
                esperaJ2_Texto.gameObject.SetActive(false);
                hudJ2.SetActive(true);

                jogador2.transform.position = GameObject.Find("Ponto Inicial (J2)").transform.position;
                jogador2.SetActive(true);
                //jogador2.GetComponent<Jogador2>().Invoke("DesativarInvencibilidade", 2f);
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Select") /*|| 
            Input.GetButtonDown("Select 2")*/ && podePausar)
        {
            pause = !pause;
        }
        if (pause == false)
        {
            menuDePausa.SetActive(false);
            Time.timeScale = escalaTemporal;
        }
        else
        {
            menuDePausa.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //MENUS
    public void Resumir()
    {
        pause = false;
    }
    public void ReiniciarCena()
    {
        StartCoroutine(MudarDeCena_Corrotina(SceneManager.GetActiveScene().name));
    }
    public void DesativarMenu(string qual)
    {
        switch (qual)
        {
            case "pausa":
                menuDePausa.SetActive(false);
                break;

            case "morte":
                podePausar = true;
                menuDeMorte.SetActive(false);
                break;
        }
    }

    void AtualizarTextosDeEspera()
    {
        switch (esperaJ1)
        {
            case "contagem":
                esperaJ1_Texto.gameObject.SetActive(true);
                esperaJ1_Texto.text = "AGUARDANDO " + (int)tempoDeEsperaJ1;
                break;

            case "pronto":
                esperaJ1_Texto.text = "PRESSIONE START!";
                break;

            case "inativo":
                esperaJ1_Texto.text = "INATIVO";
                break;
        }

        switch (esperaJ2)
        {
            case "contagem":
                esperaJ2_Texto.gameObject.SetActive(true);
                esperaJ2_Texto.text = "AGUARDANDO " + (int)tempoDeEsperaJ2;
                break;

            case "pronto":
                esperaJ2_Texto.text = "PRESSIONE START!";
                break;

            case "inativo":
                esperaJ2_Texto.text = "INATIVO";
                break;
        }
    }

    //VITÓRIA E SAÍDA
    public IEnumerator Vitória(bool direta)
    {
        if (direta == false)
        {
            foreach (GameObject jog in GameObject.FindGameObjectsWithTag("Player"))
            {
                //jog.GetComponent<Mob>().controle = Mob.Controle.nulo;
                jog.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

            yield return new WaitForSeconds(1.2f);

            GetComponent<AudioSource>().clip = jingleDeVitória;
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = false;

            yield return new WaitForSeconds(5.4f);
        }
        else
        {
            yield return new WaitForSeconds(0.6f);

            //PlayerPrefs.SetInt("Pontuacao", FindObjectOfType<Pontuação>().pontuação);
        }
    }
    //CONTROLE DE CENA
    public void MudarDeCena(string cena)
    {
        StartCoroutine(MudarDeCena_Corrotina(cena));
    }
    IEnumerator MudarDeCena_Corrotina(string cena)
    {
        print("Mudar para Cena: " + cena);

        //yield return new WaitForSeconds(2f);

        telaPreta.SetActive(true);
        telaPreta.GetComponent<Animator>().Play("Fade Out");

        yield return new WaitForSecondsRealtime(1f);

        pause = false;

        SceneManager.LoadScene(cena);
    }

    /*
    void IrParaOChefe()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            foreach (GameObject setor in setores)
            {
                setor.SetActive(false);
            }

            setores[2].SetActive(true);

            jogador1.transform.position = GameObject.Find("Ponto Inicial (J1)").transform.position;
            jogador2.transform.position = GameObject.Find("Ponto Inicial (J2)").transform.position;

            FindObjectOfType<Camera>().transform.position = GameObject.Find("Âncora (Câmera)").transform.position;
        }
    }
    */
}

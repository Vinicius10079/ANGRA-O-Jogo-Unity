using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chefe : MonoBehaviour
{
    public float delayDeInício = 2f;
    public GameObject[] asas;
    public GameObject hud;
    public GameObject[] ataques;
    public GameObject setDeBlocos;
    public float valorDeDano;
    public Material materialDeDano_Gráfico;
    public GameObject sfx_Dano;
    public float delayDeMorte = 2f;
    public Transform saídaDeAtaques;
    public GameObject lampejo;
    public AudioClip[] músicas;

    bool diminuirVolume;
    bool batalhaIniciada;
    bool ativo;
    float coolDown_Ataques_Inicial;

    GerenciadorDeFase gf;
    SpriteRenderer sr;
    Material material_Inicial;
    Mob mob;
    AudioSource bgm;

    void Awake()
    {
        bgm = FindObjectOfType<GerenciadorDeFase>().GetComponent<AudioSource>();
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();
        gf = FindObjectOfType<GerenciadorDeFase>();
        material_Inicial = sr.material;

        foreach (GameObject jog in GameObject.FindGameObjectsWithTag("Player"))
        {
            jog.GetComponent<Mob>().controle = Mob.Controle.nulo;
        }

        lampejo.SetActive(true);
    }

    void Start()
    {
        bgm.clip = músicas[0];
        bgm.enabled = true;
        bgm.Play();

        Invoke("IniciarBatalha", delayDeInício);
    }

    void Update()
    {
        if (ativo)
        {
            if (mob.dano)
            {
                Dano();
            }
        }

        if (batalhaIniciada && GameObject.FindGameObjectsWithTag("Ataque (Chefe)").Length <= 0)
        {
            Atacar();
        }

        if (diminuirVolume)
        {
            gf.GetComponent<AudioSource>().volume = Mathf.MoveTowards(gf.GetComponent<AudioSource>().volume, 0, 0.5f * Time.deltaTime);
        }
    }

    void IniciarBatalha()
    {
        foreach (GameObject jog in GameObject.FindGameObjectsWithTag("Player"))
        {
            jog.GetComponent<Mob>().controle = Mob.Controle.jogador;
            ativo = true;
        }

        bgm.clip = músicas[1];
        bgm.Play();
        setDeBlocos.SetActive(true);
        hud.SetActive(true);

        batalhaIniciada = true;
    }

    void Atacar()
    {
        if (sr.enabled)
        {
            Instantiate(ataques[Random.Range(0, ataques.Length)], saídaDeAtaques.position, saídaDeAtaques.rotation);
        }
    }

    void Dano()
    {
        sr.material = materialDeDano_Gráfico;
        mob.energia.x -= mob.valorDeDano;

        if (mob.energia.x > 0)
        {
            Invoke("AplicarMaterialPadrão", 0.2f);

            if (sfx_Dano != null)
            {
                Instantiate(sfx_Dano);
            }
        }
        else
        {
            Morrer();
        }

        mob.dano = false;
    }
    void AplicarMaterialPadrão()
    {
        sr.material = material_Inicial;
    }
    void Morrer()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        mob.energia.x = 0;

        foreach (GameObject jog in GameObject.FindGameObjectsWithTag("Player"))
        {
            jog.GetComponent<Mob>().controle = Mob.Controle.nulo;
            jog.layer = LayerMask.NameToLayer("Jogador (Invencibilidade)");
        }

        mob.AdicionarAPontuação();

        lampejo.SetActive(true);
        sr.enabled = false;
        foreach (GameObject asa in asas)
        {
            asa.GetComponent<SpriteRenderer>().enabled = false;
        }

        StartCoroutine(Encerrar());
    }

    IEnumerator Encerrar()
    {
        diminuirVolume = true;
        setDeBlocos.GetComponent<Animator>().enabled = false;

        yield return new WaitForSeconds(5f);

        gf.telaPreta.SetActive(true);
        gf.telaPreta.GetComponent<Animator>().Play("Fade Out (Prolongado)");

        yield return new WaitForSeconds(delayDeMorte);

        PlayerPrefs.SetInt("Pontuacao", FindObjectOfType<Pontuação>().pontuação);
        SceneManager.LoadScene("Encerramento");
    }
}

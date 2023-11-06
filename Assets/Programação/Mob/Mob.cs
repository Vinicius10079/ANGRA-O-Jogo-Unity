using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class Mob : MonoBehaviour
{
    public enum Controle
    {
        jogador,
        cpu,
        nulo,
    }

    [HideInInspector]
    public bool dano;
    public int valorDeDano;
    public float velocidade_Inicial, velocidadeHorizontal_Inicial, velocidadeVertical_Inicial;
    public bool podeSaltar;

    [Header("1) COMUM__________________________________________________________________")]
    public Controle controle;
    public Sprite retrato;
    public Vector2 energia;
    public Vector2 munição;
    public float velocidade;
    public float velocidadeHorizontal;
    public float velocidadeVertical;
    public float forçaDeSalto;
    public bool estáNoSolo;
    public GameObject caixaDeAcerto;
    public GameObject projétilPadrão;
    public Transform saídaDeProjétil;
    public Vector2 projéteisSimultâneos;
    public float tempoDeIvulnerabilidade;
    public Color corDeIvulnerabilidade;
    public GameObject explosão;
    public bool blindado;


    [Header("2) JOGADOR________________________________________________________________")]
    public int jogador;
    public GameObject escadaAtual;
    public bool buraco;
    public bool teleportar;
    public bool sair;
    public bool vitória;
    public GameObject projétil_Poder;
    public GameObject HUD_Jogador2;
    public GameObject[] vidaJ1_Blocos;
    public GameObject[] vidaJ2_Blocos;

    [Header("3) INIMIGO________________________________________________________________")]
    public bool ativado = true;
    public Vector2 resistência_Chefe;
    public int danoDeContato;
    public Material materialDeDano_Gráfico;
    public GameObject[] itensÀDropar;
    public float tempoDeDecisão_CPU;
    public bool carregarEnergia;
    public float velocidadeDeCargaDeEnergia;
    public GameObject HUD_Chefe;
    public Sprite morte_Chefe;
    public bool bossRush;
    public bool destrancar_Comporta;
    public GameObject sfx_Dano;
    public int valor_Pontuação;
    public GameObject textoDePontuação;
    public Text textoDePontuação_Valor;
    public GameObject jingleDeVitória_bgm;
    public AudioClip chefe_bgm;

    bool saltoAutomático;
    Transform solo_Destino;

    Rigidbody2D rb;
    GerenciadorDeFase gF;
    Projétil prj;
    Pontuação pts;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pts = FindObjectOfType<Pontuação>();

        velocidade_Inicial = velocidade;
        velocidadeHorizontal_Inicial = velocidadeHorizontal;
        velocidadeVertical_Inicial = velocidadeVertical;
        //velocidadeDeCargaDeEnergia = energia.y / 2;

        if (projétilPadrão != null)
        {
            prj = projétilPadrão.GetComponent<Projétil>();
            if (prj != null)
            {
                prj.pai = gameObject;
            }
        }
    }

    void Start()
    {
        if (projétilPadrão != null)
        {
            if (projétilPadrão.GetComponent<Projétil>() != null)
            {
                projétilPadrão.GetComponent<Projétil>().pai = gameObject;
            }
        }

        if (chefe_bgm != null)
        {
            FindObjectOfType<GerenciadorDeFase>().GetComponent<AudioSource>().clip = chefe_bgm;
            FindObjectOfType<GerenciadorDeFase>().GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {

        if (energia.x > energia.y)
        {
            energia.x = energia.y;
        }

        if (vitória)
        {
            controle = Controle.nulo;
        }

        if (destrancar_Comporta)
        {
            transform.Find("Ícone").GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void TornarRigidbodyEstático()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
    }
    public void TornarRigidbodyDinâmico()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
    }

    public void AdicionarAPontuação()
    {
        textoDePontuação.transform.SetParent(null);
        textoDePontuação.GetComponent<RectTransform>().position = transform.position;
        textoDePontuação.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
        pts.pontuação = pts.pontuação + (valor_Pontuação * FindObjectOfType<Combo_Texto>().combo);
        textoDePontuação_Valor.text = (valor_Pontuação * FindObjectOfType<Combo_Texto>().combo).ToString();
        textoDePontuação.SetActive(true);
        
    }

}

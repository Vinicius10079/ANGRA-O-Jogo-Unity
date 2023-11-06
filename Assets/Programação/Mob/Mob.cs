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
    public Vector2 muni��o;
    public float velocidade;
    public float velocidadeHorizontal;
    public float velocidadeVertical;
    public float for�aDeSalto;
    public bool est�NoSolo;
    public GameObject caixaDeAcerto;
    public GameObject proj�tilPadr�o;
    public Transform sa�daDeProj�til;
    public Vector2 proj�teisSimult�neos;
    public float tempoDeIvulnerabilidade;
    public Color corDeIvulnerabilidade;
    public GameObject explos�o;
    public bool blindado;


    [Header("2) JOGADOR________________________________________________________________")]
    public int jogador;
    public GameObject escadaAtual;
    public bool buraco;
    public bool teleportar;
    public bool sair;
    public bool vit�ria;
    public GameObject proj�til_Poder;
    public GameObject HUD_Jogador2;
    public GameObject[] vidaJ1_Blocos;
    public GameObject[] vidaJ2_Blocos;

    [Header("3) INIMIGO________________________________________________________________")]
    public bool ativado = true;
    public Vector2 resist�ncia_Chefe;
    public int danoDeContato;
    public Material materialDeDano_Gr�fico;
    public GameObject[] itens�Dropar;
    public float tempoDeDecis�o_CPU;
    public bool carregarEnergia;
    public float velocidadeDeCargaDeEnergia;
    public GameObject HUD_Chefe;
    public Sprite morte_Chefe;
    public bool bossRush;
    public bool destrancar_Comporta;
    public GameObject sfx_Dano;
    public int valor_Pontua��o;
    public GameObject textoDePontua��o;
    public Text textoDePontua��o_Valor;
    public GameObject jingleDeVit�ria_bgm;
    public AudioClip chefe_bgm;

    bool saltoAutom�tico;
    Transform solo_Destino;

    Rigidbody2D rb;
    GerenciadorDeFase gF;
    Proj�til prj;
    Pontua��o pts;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pts = FindObjectOfType<Pontua��o>();

        velocidade_Inicial = velocidade;
        velocidadeHorizontal_Inicial = velocidadeHorizontal;
        velocidadeVertical_Inicial = velocidadeVertical;
        //velocidadeDeCargaDeEnergia = energia.y / 2;

        if (proj�tilPadr�o != null)
        {
            prj = proj�tilPadr�o.GetComponent<Proj�til>();
            if (prj != null)
            {
                prj.pai = gameObject;
            }
        }
    }

    void Start()
    {
        if (proj�tilPadr�o != null)
        {
            if (proj�tilPadr�o.GetComponent<Proj�til>() != null)
            {
                proj�tilPadr�o.GetComponent<Proj�til>().pai = gameObject;
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

        if (vit�ria)
        {
            controle = Controle.nulo;
        }

        if (destrancar_Comporta)
        {
            transform.Find("�cone").GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void TornarRigidbodyEst�tico()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
    }
    public void TornarRigidbodyDin�mico()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
    }

    public void AdicionarAPontua��o()
    {
        textoDePontua��o.transform.SetParent(null);
        textoDePontua��o.GetComponent<RectTransform>().position = transform.position;
        textoDePontua��o.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
        pts.pontua��o = pts.pontua��o + (valor_Pontua��o * FindObjectOfType<Combo_Texto>().combo);
        textoDePontua��o_Valor.text = (valor_Pontua��o * FindObjectOfType<Combo_Texto>().combo).ToString();
        textoDePontua��o.SetActive(true);
        
    }

}

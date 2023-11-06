using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Padrão : MonoBehaviour
{
    public enum Tipo
    {
        corpoACorpo,
        atirador,
        defensor
    }

    public Tipo tipo;
    public GameObject jogadorAlvo;
    public GameObject[] jogadores;
    public bool decidindo;
    public bool perseguirJogador, andar_Esquerda;
    public float tempoDeCorrida;
    public bool atacar;
    public bool pular;
    public GameObject golpe_sfx;

    bool jogadorDetectado;
    bool buraco;
    float tempoDeCorrida_Inicial;

    Mob mob;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    Transform jogador;
    Material material_Inicial;

    public UnityEvent onIncrementarInimigo;

    void Awake()
    {
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        tempoDeCorrida_Inicial = tempoDeCorrida;
        material_Inicial = sr.material;
    }

    void Start()
    {
        IniciarCorrotina_Decisão();

        jogadores = GameObject.FindGameObjectsWithTag("Player");
        if (jogadores.Length > 0)
        {
            jogadorAlvo = jogadores[Random.Range(0, jogadores.Length - 1)];
        }
    }

    void Update()
    {
        if (mob.ativado)
        {
            anim.SetFloat("Velocidade Horizontal", Mathf.Abs(rb.velocity.x));
            anim.SetBool("No Solo", mob.estáNoSolo);

            if (perseguirJogador)
            {
                if (jogadorAlvo == null)
                {
                    jogadores = GameObject.FindGameObjectsWithTag("Player");
                    if (jogadores.Length > 0)
                    {
                        jogadorAlvo = jogadores[Random.Range(0, jogadores.Length)];
                    }
                }
                else
                {
                    if (jogadorAlvo.transform.position.x > transform.position.x + 0.3)
                    {
                        transform.eulerAngles = new Vector3(0, 180);
                        rb.velocity = new Vector2(mob.velocidade, rb.velocity.y);
                    }
                    else if (jogadorAlvo.transform.position.x < transform.position.x - 0.3)
                    {
                        transform.eulerAngles = Vector3.zero;
                        rb.velocity = new Vector2(-mob.velocidade, rb.velocity.y);
                    }

                    if (Mathf.Abs(jogadorAlvo.transform.position.x) < Mathf.Abs(transform.position.x + 3) &&
                        Mathf.Abs(jogadorAlvo.transform.position.y) < Mathf.Abs(transform.position.y + 1))
                    {
                        print("esq");
                        tempoDeCorrida = 0;
                    }
                }

                if (tempoDeCorrida > 0 && Mathf.Abs(jogadorAlvo.transform.position.x - transform.position.x) > 1)
                {
                    tempoDeCorrida -= Time.deltaTime;
                }
                else
                {
                    tempoDeCorrida = 0;
                    rb.velocity = Vector3.zero;
                    perseguirJogador = false;
                    IniciarCorrotina_Decisão();
                }
            }

            if (atacar && mob.estáNoSolo /*&& mob.projéteisSimultâneos.x < mob.projéteisSimultâneos.y*/)
            {
                if (jogadorAlvo != null)
                {
                    if (jogadorAlvo.transform.position.x > transform.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 180);
                    }
                    else if (jogadorAlvo.transform.position.x < transform.position.x)
                    {
                        transform.eulerAngles = Vector3.zero;
                    }
                }

                anim.SetTrigger("Ataque");

                if (golpe_sfx != null)
                {
                    Instantiate(golpe_sfx);
                }

                atacar = false;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Atacando"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (mob.dano)
        {
            Dano();
        }
    }

    void OnBecameVisible()
    {
        if (mob.ativado == false)
        {
            IniciarCorrotina_Decisão();

            mob.ativado = true;
        }
    }
    void OnBecameInvisible()
    {
        mob.ativado = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Buraco")
        {
            buraco = true;
            Morrer();
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sólido")
        {
            mob.estáNoSolo = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sólido")
        {
            mob.estáNoSolo = false;
        }
    }
    
    void AplicarMaterialPadrão()
    {
        sr.material = material_Inicial;
    }
    public void Disparar()
    {
        //mob.projétilPadrão.GetComponent<SpriteRenderer>().sortingOrder = sr.sortingOrder + 1;
        Instantiate(mob.projétilPadrão, mob.saídaDeProjétil.position, mob.saídaDeProjétil.rotation);
    }
    public void AtivarCaixaDeAcerto()
    {
        mob.caixaDeAcerto.GetComponent<CaixaDeAcerto>().âncora = mob.saídaDeProjétil.transform;
        mob.caixaDeAcerto.SetActive(true);
    }
    public void DesativarCaixaDeAcerto()
    {
        mob.caixaDeAcerto.SetActive(false);
    }
    public void IniciarCorrotina_Decisão()
    {
        StartCoroutine(Decisão());
    }

    void Dano()
    {
        sr.material = mob.materialDeDano_Gráfico;

        mob.energia.x -= mob.valorDeDano;

        if (mob.energia.x > 0)
        {
            Invoke("AplicarMaterialPadrão", 0.2f);

            if (mob.sfx_Dano != null)
            {
                Instantiate(mob.sfx_Dano);
            }

        }
        else
        {
            Morrer();
        }

        mob.dano = false;
    }
    void Morrer()
    {
        StopCoroutine(Decisão());

        if (mob.explosão != null && buraco == false)
        {
            Instantiate(mob.explosão, transform.position, transform.rotation);
            mob.AdicionarAPontuação();
        }

        Destroy(mob.caixaDeAcerto);

        if ((int)Random.Range(0, 100f) > 69)
        {
            Instantiate(mob.itensÀDropar[(int)Random.Range(0f, mob.itensÀDropar.Length)], transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    IEnumerator Decisão()
    {
        decidindo = true;
        int resultado;
        resultado = Random.Range(0, 2);

        jogadores = GameObject.FindGameObjectsWithTag("Player");
        if (jogadores.Length > 0)
        {
            jogadorAlvo = jogadores[Random.Range(0, jogadores.Length - 1)];
        }

        if (mob.estáNoSolo)
        {
            tempoDeCorrida = tempoDeCorrida_Inicial;

            yield return new WaitForSeconds(0.6f);

            switch (resultado)
            {
                case 0:
                    if (tipo != Tipo.corpoACorpo)
                    {
                        IniciarCorrotina_Decisão();
                    }
                    else
                    {
                        if (Mathf.Abs(jogadorAlvo.transform.position.x - transform.position.x) > 2)
                        {
                            perseguirJogador = true;
                        }
                        else
                        {
                            atacar = true;
                        }
                    }
                    break;

                case 1:
                    if (tipo == Tipo.corpoACorpo)
                    {
                        if (Mathf.Abs(jogadorAlvo.transform.position.x - transform.position.x) > 2)
                        {
                            perseguirJogador = true;
                        }
                        else
                        {
                            atacar = true;
                        }
                    }
                    else
                    {
                        atacar = true;
                    }
                    break;
            }
        }
        else
        {
            perseguirJogador = false;
            atacar = false;
            Invoke("IniciarCorrotina_Decisão", 0.2f);
        }

        decidindo = false;
    }
}

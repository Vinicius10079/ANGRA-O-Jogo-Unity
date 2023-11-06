using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Padr�o : MonoBehaviour
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
        IniciarCorrotina_Decis�o();

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
            anim.SetBool("No Solo", mob.est�NoSolo);

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
                    IniciarCorrotina_Decis�o();
                }
            }

            if (atacar && mob.est�NoSolo /*&& mob.proj�teisSimult�neos.x < mob.proj�teisSimult�neos.y*/)
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
            IniciarCorrotina_Decis�o();

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
        if (collision.gameObject.tag == "S�lido")
        {
            mob.est�NoSolo = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "S�lido")
        {
            mob.est�NoSolo = false;
        }
    }
    
    void AplicarMaterialPadr�o()
    {
        sr.material = material_Inicial;
    }
    public void Disparar()
    {
        //mob.proj�tilPadr�o.GetComponent<SpriteRenderer>().sortingOrder = sr.sortingOrder + 1;
        Instantiate(mob.proj�tilPadr�o, mob.sa�daDeProj�til.position, mob.sa�daDeProj�til.rotation);
    }
    public void AtivarCaixaDeAcerto()
    {
        mob.caixaDeAcerto.GetComponent<CaixaDeAcerto>().�ncora = mob.sa�daDeProj�til.transform;
        mob.caixaDeAcerto.SetActive(true);
    }
    public void DesativarCaixaDeAcerto()
    {
        mob.caixaDeAcerto.SetActive(false);
    }
    public void IniciarCorrotina_Decis�o()
    {
        StartCoroutine(Decis�o());
    }

    void Dano()
    {
        sr.material = mob.materialDeDano_Gr�fico;

        mob.energia.x -= mob.valorDeDano;

        if (mob.energia.x > 0)
        {
            Invoke("AplicarMaterialPadr�o", 0.2f);

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
        StopCoroutine(Decis�o());

        if (mob.explos�o != null && buraco == false)
        {
            Instantiate(mob.explos�o, transform.position, transform.rotation);
            mob.AdicionarAPontua��o();
        }

        Destroy(mob.caixaDeAcerto);

        if ((int)Random.Range(0, 100f) > 69)
        {
            Instantiate(mob.itens�Dropar[(int)Random.Range(0f, mob.itens�Dropar.Length)], transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    IEnumerator Decis�o()
    {
        decidindo = true;
        int resultado;
        resultado = Random.Range(0, 2);

        jogadores = GameObject.FindGameObjectsWithTag("Player");
        if (jogadores.Length > 0)
        {
            jogadorAlvo = jogadores[Random.Range(0, jogadores.Length - 1)];
        }

        if (mob.est�NoSolo)
        {
            tempoDeCorrida = tempoDeCorrida_Inicial;

            yield return new WaitForSeconds(0.6f);

            switch (resultado)
            {
                case 0:
                    if (tipo != Tipo.corpoACorpo)
                    {
                        IniciarCorrotina_Decis�o();
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
            Invoke("IniciarCorrotina_Decis�o", 0.2f);
        }

        decidindo = false;
    }
}

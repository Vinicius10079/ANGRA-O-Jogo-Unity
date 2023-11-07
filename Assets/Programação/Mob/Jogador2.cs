using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador2 : MonoBehaviour
{
    public GameObject salto_Sfx;
    public GameObject pouso_Sfx;
    public GameObject ataque_Voz_Sfx;
    public GameObject dano_Voz_Sfx;
    public GameObject morte_Voz_Sfx;
    bool sair;

    GerenciadorDeFase gf;
    Mob mob;
    Mob.Controle controle_Inicial;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;

    Combo_Texto combo_Texto;
    Pontuação pts;
    GameObject[] projéteisAtivos;

    Color cor_Inicial;

    void Awake()
    {
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gf = FindObjectOfType<GerenciadorDeFase>();
        combo_Texto = FindObjectOfType<Combo_Texto>();
        pts = FindObjectOfType<Pontuação>();

        cor_Inicial = sr.color;
        controle_Inicial = mob.controle;
        transform.position = GameObject.Find("Ponto Inicial (J" + mob.jogador + ")").transform.position;
    }

    void Start()
    {
        Invoke("Iniciar", 1);
    }

    void Update()
    {
        anim.SetFloat("Velocidade", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Solo", mob.estáNoSolo);

        
        if (mob.sair)
        {
            anim.Play("Teleporte");
            transform.Translate(Vector2.up * mob.velocidadeVertical * Time.deltaTime);
        }

        if (mob.controle == Mob.Controle.jogador)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Atacando"))
            {
                Movimentar();
                Atacar();
                Saltar();
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (mob.dano)
        {
            StartCoroutine(Dano());
        }

        if (mob.teleportar)
        {
            gameObject.layer = LayerMask.NameToLayer("Jogador (Invencibilidade)");
            anim.Play("À Teleportar");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Buraco")
        {
            mob.buraco = true;
            Morrer();
        }

        if (collision.gameObject.tag == "Sólido")
        {
            Instantiate(pouso_Sfx, transform.position, transform.rotation);
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

    void Iniciar()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        mob.controle = Mob.Controle.jogador;
        gameObject.layer = LayerMask.NameToLayer("Jogador");
        gf.podePausar = true;
    }

    void Movimentar()
    {
        if (Input.GetKey(KeyCode.LeftArrow) /*|| Input.GetAxis("Horizontal 2") < -0.5f*/)
        {
            transform.eulerAngles = new Vector3(0, 180);
            rb.velocity = new Vector2(-Mathf.Abs(mob.velocidade), rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.RightArrow) /*|| Input.GetAxis("Horizontal 2") > 0.5f*/)
        {
            transform.eulerAngles = Vector3.zero;
            rb.velocity = new Vector2(Mathf.Abs(mob.velocidade), rb.velocity.y);
        }

        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) /*&&
            Input.GetAxis("Horizontal 2") < 0.5 && Input.GetAxis("Horizontal 2") > -0.5*/)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void Atacar()
    {
        int resultado;
        resultado = (int)Random.Range(0, 7f);

        if (Input.GetKeyDown(KeyCode.U) /*|| Input.GetButtonDown("Oeste 2")*/)
        {
            mob.projétilPadrão.GetComponent<Projétil>().pai = gameObject;

            anim.SetTrigger("Ataque");

            if (resultado > 3)
            {
                Instantiate(ataque_Voz_Sfx, transform.position, transform.rotation);
            }
        }

        
    }
    public void Instanciar_Projétil()
    {
        Instantiate(mob.projétilPadrão, mob.saídaDeProjétil.position, mob.saídaDeProjétil.rotation);
    }
    void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.J) /*|| Input.GetButtonDown("Sul 2")*/)
        {
            if (mob.estáNoSolo)
            {
                rb.velocity = new Vector2(rb.velocity.x, mob.forçaDeSalto);
                Instantiate(salto_Sfx, transform.position, transform.rotation);
            }
        }
    }

    IEnumerator Dano()
    {
        if (mob.energia.x > 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Jogador (Invencibilidade)");
            mob.dano = false;
            mob.energia.x -= mob.valorDeDano;

            if (FindObjectOfType<FunçõesDeCâmera>().balançandoCâmera == false)
            {
                FindObjectOfType<FunçõesDeCâmera>().balançarCâmera = true;
            }

            combo_Texto.duração = -1;
            pts.pontuação = pts.pontuação - 800;

            //Instantiate(dano);

            if (mob.energia.x <= 0)
            {
                mob.energia.x = 0;

                Morrer();
            }
            else
            {
                Instantiate(dano_Voz_Sfx, transform.position, transform.rotation);
            }

            sr.color = mob.corDeIvulnerabilidade;
            Invoke("RetomarOControle", 0.5f);

            yield return new WaitForSeconds(mob.tempoDeIvulnerabilidade);

            DesativarInvencibilidade();
        }

    }
    void Morrer()
    {
        transform.parent = null;

        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            if (mob.jogador == 1)
            {
                gf.hudJ1.SetActive(false);
                gf.esperaJ1 = "iniciar";
            }
            if (mob.jogador == 2)
            {
                gf.hudJ2.SetActive(false);
                gf.esperaJ2 = "iniciar";
            }
        }
        else
        {
            if (mob.jogador == 1)
            {
                gf.hudJ1.SetActive(false);
                
            }
            if (mob.jogador == 2)
            {
                gf.hudJ2.SetActive(false);
                
            }

            gf.esperaJ1 = "inativo";
            gf.esperaJ2 = "inativo";
            gf.esperaJ2_Texto.gameObject.SetActive(true);

            gf.ReiniciarCena();
        }

        mob.energia.x = mob.energia.y;

        RetomarOControle();

        if (mob.buraco == false && mob.explosão != null)
        {
            Instantiate(mob.explosão);
        }

        Instantiate(morte_Voz_Sfx, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }

    void RetomarOControle()
    {
        mob.controle = controle_Inicial;
    }
    public void RetirarControle()
    {
        mob.controle = Mob.Controle.nulo;
    }
    public void ZerarVelocidade()
    {
        mob.velocidade = 0;
        rb.velocity = Vector2.zero;
    }
    public void ResetarVelocidade()
    {
        mob.velocidade = mob.velocidade_Inicial;
    }
    public void DesativarInvencibilidade()
    {
        sr.color = cor_Inicial;
        gameObject.layer = LayerMask.NameToLayer("Jogador");
    }
    public void HabilitarColisoresDeCaixa()
    {
        foreach (BoxCollider2D bc in GetComponents<BoxCollider2D>())
        {
            bc.enabled = true;
        }
    }
    public void Sair()
    {
        mob.teleportar = false;
        rb.gravityScale = 0;
        foreach (BoxCollider2D bc in GetComponents<BoxCollider2D>())
        {
            bc.enabled = false;
        }
        mob.sair = true;
    }
}

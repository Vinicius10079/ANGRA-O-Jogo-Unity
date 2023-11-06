using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomemPulga : MonoBehaviour
{
    public GameObject jogadorAlvo;
    public GameObject[] jogadores;
    public Sprite[] sprites;
    public float tempoAtéPróximoSalto;

    float tempoAtéPróximoSalto_I;
    bool buraco;

    Mob mob;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;
    Transform jogador;
    Material material_Inicial;

    void Awake()
    {
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        material_Inicial = sr.material;
        tempoAtéPróximoSalto_I = tempoAtéPróximoSalto;
    }

    void Update()
    {
        if (mob.estáNoSolo)
        {
            tempoAtéPróximoSalto -= Time.deltaTime;
            sr.sprite = sprites[0];

            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            tempoAtéPróximoSalto = tempoAtéPróximoSalto_I;
            sr.sprite = sprites[1];
        }

        if (tempoAtéPróximoSalto <= 0)
        {
            tempoAtéPróximoSalto = 0;

            Salto();
        }

        if (mob.dano)
        {
            Morrer();
        }
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

    void Salto()
    {
        EncontrarJogadorAlvo();

        if (jogadorAlvo.transform.position.x >= transform.position.x)
        {
            sr.flipX = true;
            rb.velocity = new Vector2(mob.velocidade, mob.forçaDeSalto);
        }
        else
        {
            sr.flipX = false;
            rb.velocity = new Vector2(-mob.velocidade, mob.forçaDeSalto);
        }
        
    }
    void EncontrarJogadorAlvo()
    {
        jogadores = GameObject.FindGameObjectsWithTag("Player");
        if (jogadores.Length > 0)
        {
            jogadorAlvo = jogadores[Random.Range(0, jogadores.Length)];
        }
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
    void AplicarMaterialPadrão()
    {
        sr.material = material_Inicial;
    }
    void Morrer()
    {
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
}

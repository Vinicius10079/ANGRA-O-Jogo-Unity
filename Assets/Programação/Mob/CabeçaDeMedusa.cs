using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabeçaDeMedusa : MonoBehaviour
{
    public GameObject jogadorAlvo;
    public GameObject[] jogadores;

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
    }

    void Start()
    {
        EncontrarJogadorAlvo();
    }

    void Update()
    {
        if (jogadorAlvo != null)
        {
            if (jogadorAlvo.transform.position.x > transform.position.x + 1)
            {
                transform.eulerAngles = new Vector3(0, 180);
                rb.velocity = new Vector2(mob.velocidade, rb.velocity.y);
            }
            else if (jogadorAlvo.transform.position.x < transform.position.x - 1)
            {
                transform.eulerAngles = Vector3.zero;
                rb.velocity = new Vector2(-mob.velocidade, rb.velocity.y);
            }

            if (jogadorAlvo.transform.position.y > transform.position.y + 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, mob.velocidade);
            }
            else if (jogadorAlvo.transform.position.y < transform.position.y - 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, -mob.velocidade);
            }
        }
        else
        {
            EncontrarJogadorAlvo();
        }

        if (mob.dano)
        {
            Dano();
        }
    }

    void EncontrarJogadorAlvo()
    {
        jogadores = GameObject.FindGameObjectsWithTag("Player");
        if (jogadores.Length > 0)
        {
            jogadorAlvo = jogadores[Random.Range(0, jogadores.Length - 1)];
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
    void Morrer()
    {
        if (mob.explosão != null)
        {
            Instantiate(mob.explosão, transform.position, transform.rotation);
            mob.AdicionarAPontuação();
        }

        Destroy(mob.caixaDeAcerto);

        FindObjectOfType<Setor>().inimigosDestruidos.x++;
        Destroy(gameObject);
    }
}

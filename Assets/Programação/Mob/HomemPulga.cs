using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomemPulga : MonoBehaviour
{
    public GameObject jogadorAlvo;
    public GameObject[] jogadores;
    public Sprite[] sprites;
    public float tempoAt�Pr�ximoSalto;

    float tempoAt�Pr�ximoSalto_I;
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
        tempoAt�Pr�ximoSalto_I = tempoAt�Pr�ximoSalto;
    }

    void Update()
    {
        if (mob.est�NoSolo)
        {
            tempoAt�Pr�ximoSalto -= Time.deltaTime;
            sr.sprite = sprites[0];

            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            tempoAt�Pr�ximoSalto = tempoAt�Pr�ximoSalto_I;
            sr.sprite = sprites[1];
        }

        if (tempoAt�Pr�ximoSalto <= 0)
        {
            tempoAt�Pr�ximoSalto = 0;

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

    void Salto()
    {
        EncontrarJogadorAlvo();

        if (jogadorAlvo.transform.position.x >= transform.position.x)
        {
            sr.flipX = true;
            rb.velocity = new Vector2(mob.velocidade, mob.for�aDeSalto);
        }
        else
        {
            sr.flipX = false;
            rb.velocity = new Vector2(-mob.velocidade, mob.for�aDeSalto);
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
    void AplicarMaterialPadr�o()
    {
        sr.material = material_Inicial;
    }
    void Morrer()
    {
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
}

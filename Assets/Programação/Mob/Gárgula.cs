using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SEPARAR "CAIXA DE ACERTO" DO "OBJETO" PARA DIFERENCIAR O SEU "COLISOR TRIGGER" DO "DETECTOR DE SOLO".

public class G�rgula : MonoBehaviour
{
    public GameObject jogadorAlvo;
    public GameObject[] jogadores;
    public Sprite[] sprites;
    public float tempoAt�Pr�ximoSalto;
    public GameObject salto_sfx;
    public GameObject pouso_sfx;

    bool buraco;
    float tempoAt�Pr�ximoSalto_I;

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

    void Start()
    {
        EncontrarJogadorAlvo();
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
            tempoAt�Pr�ximoSalto = Random.Range(tempoAt�Pr�ximoSalto_I, tempoAt�Pr�ximoSalto_I * 2);
            sr.sprite = sprites[1];
        }

        if (tempoAt�Pr�ximoSalto <= 0)
        {
            tempoAt�Pr�ximoSalto = 0;

            Salto();
        }

        if (mob.dano)
        {
            Dano();
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
            if (mob.est�NoSolo == false && pouso_sfx != null)
            {
                Instantiate(pouso_sfx);
            }
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

    void EncontrarJogadorAlvo()
    {
        jogadores = GameObject.FindGameObjectsWithTag("Player");
        if (jogadores.Length > 0)
        {
            jogadorAlvo = jogadores[Random.Range(0, jogadores.Length)];
        }
    }

    void Salto()
    {
        EncontrarJogadorAlvo();

        if (jogadorAlvo.transform.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(mob.velocidade, mob.for�aDeSalto);
            transform.eulerAngles = new Vector3(0, 180);
        }
        else if (jogadorAlvo.transform.position.x <= transform.position.x)
        {
            rb.velocity = new Vector2(-mob.velocidade, mob.for�aDeSalto);
            transform.eulerAngles = Vector3.zero;
        }
        
        if (salto_sfx != null)
        {
            Instantiate(salto_sfx);
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

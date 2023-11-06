using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morcego : MonoBehaviour
{
    public float tempoDeRepouso;
    public GameObject colis„o_sfx;

    bool Voltar¿SeMover_;
    bool direÁ„oOposta;
    float velocidade_Inicial;
    Material material_Inicial;

    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D rb;
    Mob mob;

    void Awake()
    {
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        velocidade_Inicial = mob.velocidade;
        material_Inicial = sr.material;
    }

    void Update()
    {
        if (mob.ativado)
        {
            if (direÁ„oOposta == false)
            {
                rb.velocity = new Vector2(0, mob.velocidade);
            }
            else
            {
                rb.velocity = new Vector2(0, -mob.velocidade);
            }

            if (mob.dano == true)
            {
                Dano();
            }
        }
    }

    void OnBecameVisible()
    {
        mob.ativado = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SÛlido"))
        {
            if (colis„o_sfx != null)
            {
                Instantiate(colis„o_sfx);
            }

            mob.velocidade = 0;
            rb.velocity = Vector2.zero;
            anim.enabled = true;
            direÁ„oOposta = !direÁ„oOposta;

            Invoke("Voltar¿SeMover", tempoDeRepouso);
        }
    }

    void Voltar¿SeMover()
    {
        mob.velocidade = velocidade_Inicial;
        anim.enabled = true;
        Voltar¿SeMover_ = false;
    }

    void AplicarMaterialPadr„o()
    {
        sr.material = material_Inicial;
    }
    void Dano()
    {
        sr.material = mob.materialDeDano_Gr·fico;
        mob.energia.x -= mob.valorDeDano;

        if (mob.energia.x > 0)
        {
            Invoke("AplicarMaterialPadr„o", 0.2f);
            mob.dano = false;
        }
        else
        {
            Morrer();
        }
    }
    void Morrer()
    {
        if (mob.explos„o != null)
        {
            Instantiate(mob.explos„o, transform.position, transform.rotation);
            mob.AdicionarAPontuaÁ„o();
        }

        if ((int)Random.Range(0, 100f) > 69)
        {
            Instantiate(mob.itens¿Dropar[(int)Random.Range(0f, mob.itens¿Dropar.Length)], transform.position, transform.rotation);
        }


        Destroy(gameObject);
    }
}

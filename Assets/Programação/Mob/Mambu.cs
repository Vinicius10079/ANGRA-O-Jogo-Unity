using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mambu : MonoBehaviour
{
    public bool movimentar;
    public Sprite[] sprites;
    public float tempoDeVÙo = 1.2f;

    public GameObject disparo_sfx;

    float tempoDeVÙo_Inicial;
    bool atirou;

    Mob mob;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Material material_Inicial;

    void Awake()
    {
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();

        tempoDeVÙo_Inicial = tempoDeVÙo;
        material_Inicial = sr.material;
    }

    void Update()
    {
        if (movimentar)
        {
            sr.sprite = sprites[1];

            transform.Translate(Vector2.left * mob.velocidadeHorizontal * Time.deltaTime);

            if (tempoDeVÙo > 0)
            {
                tempoDeVÙo -= Time.deltaTime;
            }
            else
            {
                tempoDeVÙo = 0;
                movimentar = false;
            }
        }
        else if (atirou == false)
        {
            tempoDeVÙo = tempoDeVÙo_Inicial;

            sr.sprite = sprites[0];

            Disparar();

            Invoke("Movimentar", 0.8f);

            atirou = true;
        }

        if (sr.sprite == sprites[0])
        {
            mob.blindado = false;
        }
        else if (sr.sprite == sprites[1])
        {
            mob.blindado = true;
        }

        if (mob.dano)
        {
            Dano();
        }
    }

    void OnBecameVisible()
    {
        mob.ativado = true;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Disparar()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(mob.projÈtilPadr„o, mob.saÌdaDeProjÈtil.position, mob.saÌdaDeProjÈtil.rotation);
            mob.saÌdaDeProjÈtil.transform.eulerAngles = new Vector3(0, 0, mob.saÌdaDeProjÈtil.transform.eulerAngles.z + 45);
        }

        if (disparo_sfx)
        {
            Instantiate(disparo_sfx);
        }
    }

    void Movimentar()
    {
        movimentar = true;
        atirou = false;
    }

    //MÛdulo de PrejuÌzo
    void Dano()
    {
        sr.material = mob.materialDeDano_Gr·fico;

        mob.energia.x -= mob.valorDeDano;

        if (mob.energia.x > 0)
        {
            Invoke("AplicarMaterialPadr„o", 0.2f);

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
    void AplicarMaterialPadr„o()
    {
        sr.material = material_Inicial;
    }
    void Morrer()
    {
        if (mob.explos„o != null)
        {
            Instantiate(mob.explos„o, transform.position, transform.rotation);
        }

        if ((int)Random.Range(0, 100f) > 69)
        {
            Instantiate(mob.itens¿Dropar[(int)Random.Range(0f, mob.itens¿Dropar.Length)], transform.position, transform.rotation);
        }

        FindObjectOfType<Setor>().inimigosDestruidos.x++;

        Destroy(gameObject);
    }
}

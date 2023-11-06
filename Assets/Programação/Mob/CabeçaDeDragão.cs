using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabeçaDeDragão : MonoBehaviour
{
    public float duraçãoDeAção = 1.6f;
    public float intervaloDeDisparo = 0.8f;
    public Sprite[] sprites;

    float duraçãoDeAção_Inicial;
    bool funçãoIniciada;

    Mob mob;
    SpriteRenderer sr;
    Animator anim;
    Material material_Inicial;

    void Awake()
    {
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        duraçãoDeAção_Inicial = duraçãoDeAção;
        material_Inicial = sr.material;
    }

    void Update()
    {
        if (sr.sprite == sprites[0])
        {
            if (duraçãoDeAção > 0)
            {
                duraçãoDeAção -= Time.deltaTime;
            }
            else
            {
                if (funçãoIniciada == false)
                {
                    Disparo();
                    funçãoIniciada = true;
                }

                duraçãoDeAção = duraçãoDeAção_Inicial;
                sr.sprite = sprites[1];
            }

            
        }
        else if (sr.sprite == sprites[1])
        {
            if (duraçãoDeAção > 0)
            {
                duraçãoDeAção -= Time.deltaTime;
            }
            else
            {
                duraçãoDeAção = duraçãoDeAção_Inicial;
                funçãoIniciada = false;
                sr.sprite = sprites[0];
            }
        }

        if (mob.dano)
        {
            Dano();
        }
    }

    void Disparo()
    {
        Instantiate(mob.projétilPadrão, mob.saídaDeProjétil.position, mob.saídaDeProjétil.rotation);
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
        if (mob.explosão != null)
        {
            Instantiate(mob.explosão, transform.position, transform.rotation);
            mob.AdicionarAPontuação();
        }

        if ((int)Random.Range(0, 100f) > 69)
        {
            Instantiate(mob.itensÀDropar[(int)Random.Range(0f, mob.itensÀDropar.Length)], transform.position, transform.rotation);
        }


        Destroy(gameObject);
    }
}

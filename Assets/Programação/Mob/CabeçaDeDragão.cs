using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabe�aDeDrag�o : MonoBehaviour
{
    public float dura��oDeA��o = 1.6f;
    public float intervaloDeDisparo = 0.8f;
    public Sprite[] sprites;

    float dura��oDeA��o_Inicial;
    bool fun��oIniciada;

    Mob mob;
    SpriteRenderer sr;
    Animator anim;
    Material material_Inicial;

    void Awake()
    {
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        dura��oDeA��o_Inicial = dura��oDeA��o;
        material_Inicial = sr.material;
    }

    void Update()
    {
        if (sr.sprite == sprites[0])
        {
            if (dura��oDeA��o > 0)
            {
                dura��oDeA��o -= Time.deltaTime;
            }
            else
            {
                if (fun��oIniciada == false)
                {
                    Disparo();
                    fun��oIniciada = true;
                }

                dura��oDeA��o = dura��oDeA��o_Inicial;
                sr.sprite = sprites[1];
            }

            
        }
        else if (sr.sprite == sprites[1])
        {
            if (dura��oDeA��o > 0)
            {
                dura��oDeA��o -= Time.deltaTime;
            }
            else
            {
                dura��oDeA��o = dura��oDeA��o_Inicial;
                fun��oIniciada = false;
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
        Instantiate(mob.proj�tilPadr�o, mob.sa�daDeProj�til.position, mob.sa�daDeProj�til.rotation);
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
        if (mob.explos�o != null)
        {
            Instantiate(mob.explos�o, transform.position, transform.rotation);
            mob.AdicionarAPontua��o();
        }

        if ((int)Random.Range(0, 100f) > 69)
        {
            Instantiate(mob.itens�Dropar[(int)Random.Range(0f, mob.itens�Dropar.Length)], transform.position, transform.rotation);
        }


        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mambu : MonoBehaviour
{
    public bool movimentar;
    public Sprite[] sprites;
    public float tempoDeV�o = 1.2f;

    public GameObject disparo_sfx;

    float tempoDeV�o_Inicial;
    bool atirou;

    Mob mob;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Material material_Inicial;

    void Awake()
    {
        mob = GetComponent<Mob>();
        sr = GetComponent<SpriteRenderer>();

        tempoDeV�o_Inicial = tempoDeV�o;
        material_Inicial = sr.material;
    }

    void Update()
    {
        if (movimentar)
        {
            sr.sprite = sprites[1];

            transform.Translate(Vector2.left * mob.velocidadeHorizontal * Time.deltaTime);

            if (tempoDeV�o > 0)
            {
                tempoDeV�o -= Time.deltaTime;
            }
            else
            {
                tempoDeV�o = 0;
                movimentar = false;
            }
        }
        else if (atirou == false)
        {
            tempoDeV�o = tempoDeV�o_Inicial;

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
            Instantiate(mob.proj�tilPadr�o, mob.sa�daDeProj�til.position, mob.sa�daDeProj�til.rotation);
            mob.sa�daDeProj�til.transform.eulerAngles = new Vector3(0, 0, mob.sa�daDeProj�til.transform.eulerAngles.z + 45);
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

    //M�dulo de Preju�zo
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
        }

        if ((int)Random.Range(0, 100f) > 69)
        {
            Instantiate(mob.itens�Dropar[(int)Random.Range(0f, mob.itens�Dropar.Length)], transform.position, transform.rotation);
        }

        FindObjectOfType<Setor>().inimigosDestruidos.x++;

        Destroy(gameObject);
    }
}

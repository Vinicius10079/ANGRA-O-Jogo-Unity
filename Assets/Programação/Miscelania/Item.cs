using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Item_Tipo
    {
        refilDeEnergia,
    }
    public Item_Tipo tipo;
    public bool temporário;
    public Color transparente_Cor;

    [Header("SFX")]
    public GameObject sfx;

    bool colidiu;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //StartCoroutine(Animação());

        if (temporário)
        {
            StartCoroutine(Desaparecer());
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && colidiu == false)
        {
            colidiu = true;

            switch (tipo)
            {
                case Item_Tipo.refilDeEnergia:
                    collision.gameObject.GetComponent<Mob>().energia.x += 10;
                    break;
            }

            if (sfx != null)
            {
                Instantiate(sfx);
            }

            Destroy(gameObject);
        }
    }

    IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(1.2f);

        sr.color = transparente_Cor;

        yield return new WaitForSeconds(0.6f);

        Destroy(gameObject);
    }
}

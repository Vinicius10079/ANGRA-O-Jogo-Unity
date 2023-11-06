using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaDeAcerto : MonoBehaviour
{
    public bool n�oDesafiliarAoDespertar = true;
    public int dano;
    public Transform �ncora;
    public Proj�til proj�til;
    public bool destruirAoContato;

    BoxCollider2D bc;

    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();

        if (n�oDesafiliarAoDespertar == false)
        {
            transform.parent = null;
        }

        if (GetComponent<Proj�til>() != null)
        {
            proj�til = GetComponent<Proj�til>();
        }
    }

    void Update()
    {
        if (proj�til == null && �ncora != null)
        {
            transform.position = �ncora.position;
            transform.eulerAngles = �ncora.eulerAngles;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Mob mob;

        mob = collision.GetComponent<Mob>();
        if (mob != null)
        {
            if (mob.blindado == false)
            {
                mob.valorDeDano = dano;
                mob.dano = true;
            }
            else if (proj�til != null)
            {
                bc.enabled = false;
                proj�til.refletido = true;
            }
        }

        if (gameObject.tag == "Jogador (Ataque)" && collision.gameObject.tag == "Inimigo")
        {
            FindObjectOfType<Combo_Texto>().combo++;
        }

        if (destruirAoContato && bc.enabled)
        {
            if (proj�til.deforma��o != null)
            {
                Instantiate(proj�til.deforma��o, transform.position, transform.rotation);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Jogador (Ataque)"))
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Inimigo") ||
                    collision.gameObject.layer == LayerMask.NameToLayer("Inimigo (Colid�vel)") ||
                    collision.gameObject.layer == LayerMask.NameToLayer("Inimigo (Ataque)(Reflet�vel)") ||
                    collision.gameObject.layer == LayerMask.NameToLayer("S�lido"))
                {
                    Destroy(gameObject);
                }
            }

            if (gameObject.layer == LayerMask.NameToLayer("Inimigo(Ataque)(Reflet�vel)"))
            {
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaDeAcerto : MonoBehaviour
{
    public bool nãoDesafiliarAoDespertar = true;
    public int dano;
    public Transform âncora;
    public Projétil projétil;
    public bool destruirAoContato;

    BoxCollider2D bc;

    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();

        if (nãoDesafiliarAoDespertar == false)
        {
            transform.parent = null;
        }

        if (GetComponent<Projétil>() != null)
        {
            projétil = GetComponent<Projétil>();
        }
    }

    void Update()
    {
        if (projétil == null && âncora != null)
        {
            transform.position = âncora.position;
            transform.eulerAngles = âncora.eulerAngles;
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
            else if (projétil != null)
            {
                bc.enabled = false;
                projétil.refletido = true;
            }
        }

        if (gameObject.tag == "Jogador (Ataque)" && collision.gameObject.tag == "Inimigo")
        {
            FindObjectOfType<Combo_Texto>().combo++;
        }

        if (destruirAoContato && bc.enabled)
        {
            if (projétil.deformação != null)
            {
                Instantiate(projétil.deformação, transform.position, transform.rotation);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Jogador (Ataque)"))
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Inimigo") ||
                    collision.gameObject.layer == LayerMask.NameToLayer("Inimigo (Colidível)") ||
                    collision.gameObject.layer == LayerMask.NameToLayer("Inimigo (Ataque)(Refletível)") ||
                    collision.gameObject.layer == LayerMask.NameToLayer("Sólido"))
                {
                    Destroy(gameObject);
                }
            }

            if (gameObject.layer == LayerMask.NameToLayer("Inimigo(Ataque)(Refletível)"))
            {
                Destroy(gameObject);
            }
        }
    }
}

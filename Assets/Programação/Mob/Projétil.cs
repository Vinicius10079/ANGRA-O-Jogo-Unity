using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projétil : MonoBehaviour
{
    public bool moverPorFísica;
    public float velocidade = 10;
    public bool refletido;
    public GameObject deformação;
    public GameObject pai;
    public bool destruirPorTempo;
    public float destruirPorTempo_tempo;

    [Header("SFX")]
    public GameObject sfx;
    public GameObject sfx_acerto;
    public GameObject sfx_Refletido;

    bool somTocado_refletido;

    Rigidbody2D rb;
    Mob mobCol;
    Transform cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>().transform;
    }

    void Start()
    {
        if (pai != null)
        {
            pai.GetComponent<Mob>().projéteisSimultâneos.x++;
        }

        if (sfx != null)
        {
            Instantiate(sfx);
        }

        if (destruirPorTempo)
        {
            StartCoroutine(ContagemRegressiva_Destruição());
        }
    }

    void Update()
    {
        if (moverPorFísica == false)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
        }
        else
        {
            if (transform.eulerAngles.y == 0)
            {
                rb.velocity = new Vector2(velocidade, rb.velocity.y);
            }
            else if (transform.eulerAngles.y == 180)
            {
                rb.velocity = new Vector2(-velocidade, rb.velocity.y);
            }
        }
    }

    void OnBecameInvisible()
    {
        Destruir();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (sfx_acerto != null)
        {
            Instantiate(sfx_acerto);
        }

        if (collision.gameObject.tag == "Sólido")
        {
            Destruir();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Jogador (Ataque)") 
            && gameObject.layer == LayerMask.NameToLayer("Inimigo (Ataque)(Refletível)"))
        {
            Destruir();
        }
    }

    public void Destruir()
    {
        if (pai != null)
        {
            pai.GetComponent<Mob>().projéteisSimultâneos.x--;
        }
        if (deformação != null)
        {
            Instantiate(deformação, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    IEnumerator ContagemRegressiva_Destruição()
    {
        yield return new WaitForSeconds(destruirPorTempo_tempo);

        velocidade = 0;

        Destruir();
    }
}

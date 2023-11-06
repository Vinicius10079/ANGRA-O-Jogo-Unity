using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Didática : MonoBehaviour
{
    public enum Opções
    {
        opção1,
        opção2,
        opção3
    }

    public bool boleana;
    public string caractere;
    public int inteiro;
    public float real;
    public double realDuplo;
    public Opções menu;
    public Vector2 vetor2;
    public Vector3 vetor3;
    public GameObject projétil;
    public Rigidbody2D corpoRígido2D;
    public Animator animador;

    public int[] coleçãoDeInteiros;

    void Awake()
    {
        if (menu == Opções.opção1)
        {
            print("Opção 1");
        }
    }

    void Start()
    {
        if (transform.position.x == 0)
        {
            gameObject.name = "forte";
        }
    }

    void Update()
    {
        Condicionais();

        //caminhos de acesso
        real = gameObject.GetComponent<Didática>().vetor2.x;
    }

    void OnBecameVisible()
    {
        
    }
    void OnBecameInvisible()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    void OnCollisionStay2D(Collision2D collision)
    {

    }
    void OnCollisionExit2D(Collision2D collision)
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {

    }
    void OnTriggerExit2D(Collider2D collision)
    {

    }

    void OnDisable()
    {
        
    }
    void OnDestroy()
    {
        
    }

    void ClassesEMétodos_Unity()
    {
        vetor2 = new Vector2(2, 4);
        vetor3 = new Vector3(3, 6, 9);
        vetor3 = new Vector3(3, 6);

        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        if (Input.GetKeyDown("C"))
        {

        }

        if (Input.GetKeyDown(KeyCode.B))
        {

        }

        if (Input.GetKeyDown(KeyCode.C))
        {

        }

        GetComponent<Rigidbody2D>();

        GameObject.Find("Nome do Objeto");
        GameObject.FindGameObjectsWithTag("Inanimados");
        FindObjectOfType<Rigidbody2D>();

        Instantiate(projétil);

        StartCoroutine(Corrotina());
        StopCoroutine(Corrotina());
        StopAllCoroutines();
    }

    void Condicionais()
    {
        //If
        if (inteiro > 2)
        {
            print("é maior que 2.");
        }
        else if (inteiro < 0)
        {
            print("é menor que 0.");
        }
        else
        {
            print("não.");
        }

        //For
        for (int i = 5; i > 0; i--)
        {
            print(i);
        }

        foreach (int número in coleçãoDeInteiros)
        {
            print(número);
        }

        //While
        while (caractere == "")
        {
            print("caractere está vazio");
        }

        do
        {
            print("fez");
        }
        while (real > 3);
        {
            print("fez na condição");
        }

        //Switch
        switch (menu)
        {
            case Opções.opção1:
                print(1);
                break;

            case Opções.opção2:
                print(2);
                break;

            case Opções.opção3:
                print(3);
                break;
        }
    }

    void OperadoresLógicos()
    {
        if (caractere == "João" || caractere == "Paulo")
        {
            print(caractere);
        }

        if (caractere == "João" && caractere == "Paulo")
        {
            print(caractere);
        }

        if (caractere != "João")
        {
            print(caractere);
        }

        if (real > 1)
        {
            print(real);
        }

        if (real < 1)
        {
            print(real);
        }

        if (real <= 1)
        {
            print(real);
        }

        if (real >= 1)
        {
            print(real);
        }
    }

    void Animação()
    {
        GetComponent<Animator>().Play("Ataque");

        GetComponent<Animator>().SetBool("Boleano", true);
        GetComponent<Animator>().SetInteger("Inteiro", 1);
        GetComponent<Animator>().SetFloat("Real", 1.5f);
        GetComponent<Animator>().SetTrigger("Gatilho");

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pulando"))
        {

        }
    }

    IEnumerator Corrotina()
    {
        //fazer coisa.

        yield return new WaitForSeconds(1.2f);

        //fezer mais coisas.
    }
}

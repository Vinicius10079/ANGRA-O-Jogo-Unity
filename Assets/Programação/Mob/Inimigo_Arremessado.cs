using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_Arremessado : MonoBehaviour
{
    public GameObject explosão;

    int girar = 1;
    float posYInicial;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        posYInicial = transform.position.y;

        //girar = (int)Random.Range(0, 2f);
    }

    void Start()
    {
        if (transform.eulerAngles == Vector3.zero)
        {
            rb.velocity = new Vector2(4f, 12f);
        }
        else
        {
            rb.velocity = new Vector2(-4f, 12f);
        }
        if (girar == 1)
        {
            GetComponent<Animator>().enabled = true;
        }
    }

    void Update()
    {
        if (transform.position.y < posYInicial - 1f)
        {
            if (explosão != null)
            {
                Instantiate(explosão, transform.position, transform.rotation);
            }

            if (FindObjectOfType<FunçõesDeCâmera>().balançandoCâmera == false)
            {
                FindObjectOfType<FunçõesDeCâmera>().balançarCâmera = true;
            }
            
            Destroy(gameObject);
        }
    }
}

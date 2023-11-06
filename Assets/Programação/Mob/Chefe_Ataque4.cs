using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefe_Ataque4 : MonoBehaviour
{
    public float velocidade = 3f;
    public float largura = 5f;
    public float duração = 1.2f;

    bool corrotinaIniciada;
    bool aumentar = true;
    bool diminuir = false;

    GameObject[] jogadores;
    Transform alvo;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Ataque (Chefe)").Length > 0)
        {
            Destroy(gameObject);
        }
        jogadores = GameObject.FindGameObjectsWithTag("Player");
        alvo = jogadores[Random.Range(0, jogadores.Length)].transform;

        transform.position = new Vector3(alvo.position.x, FindObjectOfType<Camera>().transform.position.y);
    }

    void Update()
    {
        if (transform.localScale.x < largura)
        {
            if (aumentar)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale,
                    new Vector3(largura, transform.localScale.y), velocidade * Time.deltaTime);
            }
        }
        else
        {
            StartCoroutine(DestruirObjeto());
        }

        if (diminuir)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale,
                new Vector3(0, transform.localScale.y), velocidade * Time.deltaTime);
        }
    }

    IEnumerator DestruirObjeto()
    {
        if (corrotinaIniciada == false)
        {
            corrotinaIniciada = true;
            GetComponent<BoxCollider2D>().enabled = true;

            yield return new WaitForSeconds(duração);

            aumentar = false;
            diminuir = true;

            yield return new WaitForSeconds(velocidade);

            Destroy(gameObject);
        }
    }
}

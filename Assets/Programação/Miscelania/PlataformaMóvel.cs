using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMóvel : MonoBehaviour
{
    public Transform destino1, destino2, destinoAtual;
    public float velocidade;

    GameObject jog;

    void Awake()
    {
        destino1 = transform.GetChild(0);
        destino2 = transform.GetChild(1);
        destinoAtual = destino1;

        destino1.parent = null;
        destino2.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destinoAtual.position, velocidade * Time.deltaTime);

        if (transform.position == destino1.position)
        {
            destinoAtual = destino2;
        }
        if (transform.position == destino2.position)
        {
            destinoAtual = destino1;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y >= transform.position.y + 1.5f)
            {
                jog = collision.gameObject;
                jog.transform.parent = transform;
            }
            else if (jog != null)
            {
                jog.transform.parent = null;
                jog = null;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && jog != null)
        {
            if (jog.transform.parent != null)
            {
                jog.transform.parent = null;
            }
            
            jog = null;
        }
    }
}

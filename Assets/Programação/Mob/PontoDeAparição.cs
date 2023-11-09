using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoDeAparição : MonoBehaviour
{
    public int jogador = 1;

    void Start()
    {
        if (FindObjectOfType<Jogador1>() != null)
        {
            FindObjectOfType<Jogador1>().DesativarInvencibilidade();
        }
        if (FindObjectOfType<Jogador2>() != null)
        {
            FindObjectOfType<Jogador2>().DesativarInvencibilidade();
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Indicador_Jogador ind;

        if (collision.gameObject.tag == "Player" && jogador == collision.gameObject.GetComponent<Mob>().jogador)
        {
            ind = GameObject.Find("Indicador (J" + collision.gameObject.GetComponent<Mob>().jogador + ")").
                GetComponent<Indicador_Jogador>();

            ind.GetComponent<Animator>().enabled = true;
            collision.gameObject.GetComponent<Mob>().controle = Mob.Controle.nulo;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            StartCoroutine(IniciarControle_Jogador(collision.gameObject.GetComponent<Mob>()));
        }
    }

    IEnumerator IniciarControle_Jogador(Mob jogador)
    {
        yield return new WaitForSeconds(1f);

        jogador.controle = Mob.Controle.jogador;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

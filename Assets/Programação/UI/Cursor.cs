using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Cursor : MonoBehaviour
{
    public int jogador = 1;
    public int limiteDeOp��es;
    public bool podeMover;
    public Op��o op��oAtual;
    public GameObject confirma��o_sfx;

    public RectTransform op��o_cima;
    public RectTransform op��o_baixo;
    public RectTransform op��o_esquerda;
    public RectTransform op��o_direita;

    bool moveu;
    string moveuPara;

    Color corInicial;

    void Awake()
    {
        corInicial = GetComponent<Image>().color;

        AtualizarDire��es();
    }

    void Update()
    {
        if (jogador == 2)
        {
            Corre��oDePosicionamento_P2();
        }

        if (podeMover)
        {
            switch (jogador)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.W) && op��o_cima != null)
                    {
                        transform.position = op��o_cima.position;
                        op��oAtual = op��o_cima.GetComponent<Op��o>();

                        AtualizarDire��es();

                        moveuPara = "cima";
                    }
                    if (Input.GetKeyDown(KeyCode.S) && op��o_baixo != null)
                    {
                        transform.position = op��o_baixo.position;
                        op��oAtual = op��o_baixo.GetComponent<Op��o>();

                        AtualizarDire��es();

                        moveuPara = "baixo";
                    }
                    if (Input.GetKeyDown(KeyCode.A) && op��o_esquerda != null)
                    {
                        transform.position = op��o_esquerda.position;
                        op��oAtual = op��o_esquerda.GetComponent<Op��o>();

                        AtualizarDire��es();

                        moveuPara = "esquerda";
                    }
                    if (Input.GetKeyDown(KeyCode.D) && op��o_direita != null)
                    {
                        transform.position = op��o_direita.position;
                        op��oAtual = op��o_direita.GetComponent<Op��o>();

                        AtualizarDire��es();

                        moveuPara = "direita";
                    }

                    if (TesteDeControles.joystickHabilitado)
                    {
                        if (Input.GetAxis("Vertical") > 0.5f && op��o_cima != null && moveu == false)
                        {
                            transform.position = op��o_cima.position;
                            op��oAtual = op��o_cima.GetComponent<Op��o>();

                            AtualizarDire��es();

                            moveuPara = "cima";
                            moveu = true;
                        }
                        if (Input.GetAxis("Vertical") < -0.5f && op��o_baixo != null && moveu == false)
                        {
                            transform.position = op��o_baixo.position;
                            op��oAtual = op��o_baixo.GetComponent<Op��o>();

                            AtualizarDire��es();

                            moveuPara = "baixo";
                            moveu = true;
                        }
                        if (Input.GetAxis("Horizontal") < -0.5f && op��o_esquerda != null && moveu == false)
                        {
                            transform.position = op��o_esquerda.position;
                            op��oAtual = op��o_esquerda.GetComponent<Op��o>();

                            AtualizarDire��es();

                            moveuPara = "esquerda";
                            moveu = true;
                        }
                        if (Input.GetAxis("Horizontal") > 0.5f && op��o_direita != null && moveu == false)
                        {
                            transform.position = op��o_direita.position;
                            op��oAtual = op��o_direita.GetComponent<Op��o>();

                            AtualizarDire��es();

                            moveuPara = "direita";
                            moveu = true;
                        }

                        if (Input.GetAxis("Vertical") < 0.5f && Input.GetAxis("Vertical") > -0.5f 
                            && Input.GetAxis("Horizontal") < 0.5f && Input.GetAxis("Horizontal") > -0.5f)
                        {
                            moveu = false;
                        }
                    }
                    break;

                case 2:
                    if (Input.GetKeyDown(KeyCode.UpArrow) && op��o_cima != null)
                    {
                        transform.position = op��o_cima.position;
                        op��oAtual = op��o_cima.GetComponent<Op��o>();

                        AtualizarDire��es();

                        moveuPara = "cima";
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow) && op��o_baixo != null)
                    {
                        transform.position = op��o_baixo.position;
                        op��oAtual = op��o_baixo.GetComponent<Op��o>();

                        AtualizarDire��es();

                        moveuPara = "baixo";
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && op��o_esquerda != null)
                    {
                        transform.position = op��o_esquerda.position;
                        op��oAtual = op��o_esquerda.GetComponent<Op��o>();

                        AtualizarDire��es();

                        moveuPara = "esquerda";
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow) && op��o_direita != null)
                    {
                        transform.position = op��o_direita.position;
                        op��oAtual = op��o_direita.GetComponent<Op��o>();

                        AtualizarDire��es();

                        moveuPara = "direita";
                    }

                    if (TesteDeControles.joystickHabilitado)
                    {
                        if (Input.GetAxis("Vertical 2") > 0.5f && op��o_cima != null && moveu == false)
                        {
                            transform.position = op��o_cima.position;
                            op��oAtual = op��o_cima.GetComponent<Op��o>();

                            AtualizarDire��es();

                            moveuPara = "cima";
                            moveu = true;
                        }
                        if (Input.GetAxis("Vertical 2") < -0.5f && op��o_baixo != null && moveu == false)
                        {
                            transform.position = op��o_baixo.position;
                            op��oAtual = op��o_baixo.GetComponent<Op��o>();

                            AtualizarDire��es();

                            moveuPara = "baixo";
                            moveu = true;
                        }
                        if (Input.GetAxis("Horizontal 2") < -0.5f && op��o_esquerda != null && moveu == false)
                        {
                            transform.position = op��o_esquerda.position;
                            op��oAtual = op��o_esquerda.GetComponent<Op��o>();

                            AtualizarDire��es();

                            moveuPara = "esquerda";
                            moveu = true;
                        }
                        if (Input.GetAxis("Horizontal 2") > 0.5f && op��o_direita != null && moveu == false)
                        {
                            transform.position = op��o_direita.position;
                            op��oAtual = op��o_direita.GetComponent<Op��o>();

                            AtualizarDire��es();

                            moveuPara = "direita";
                            moveu = true;
                        }

                        if (Input.GetAxis("Vertical 2") < 0.5f && Input.GetAxis("Vertical 2") > -0.5f
                            && Input.GetAxis("Horizontal 2") < 0.5f && Input.GetAxis("Horizontal 2") > -0.5f)
                        {
                            moveu = false;
                        }
                    }
                    break;
            }
        }

        switch (jogador)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.G) && op��oAtual.op��oBloqueada == false)
                {
                    if (confirma��o_sfx != null)
                    {
                        Instantiate(confirma��o_sfx);
                    }
                    op��oAtual.evento.Invoke();
                }

                if (TesteDeControles.joystickHabilitado && Input.GetButtonDown("Sul")
                    && op��oAtual.op��oBloqueada == false)
                {
                    if (confirma��o_sfx != null)
                    {
                        Instantiate(confirma��o_sfx);
                    }
                    op��oAtual.evento.Invoke();
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.J) && op��oAtual.op��oBloqueada == false)
                {
                    if (confirma��o_sfx != null)
                    {
                        Instantiate(confirma��o_sfx);
                    }
                    op��oAtual.evento_J2.Invoke();
                }

                if (TesteDeControles.joystickHabilitado && Input.GetButtonDown("Sul 2")
                    && op��oAtual.op��oBloqueada == false)
                {
                    if (confirma��o_sfx != null)
                    {
                        Instantiate(confirma��o_sfx);
                    }
                    op��oAtual.evento.Invoke();
                }
                break;
        }

        if (op��oAtual.op��oBloqueada)
        {
            GetComponent<Image>().color = Color.clear;

            switch (moveuPara)
            {
                case "cima":
                    transform.position = op��o_cima.position;
                    op��oAtual = op��o_cima.GetComponent<Op��o>();

                    AtualizarDire��es();

                    moveuPara = "cima";
                    break;

                case "baixo":
                    transform.position = op��o_baixo.position;
                    op��oAtual = op��o_baixo.GetComponent<Op��o>();

                    AtualizarDire��es();

                    moveuPara = "baixo";
                    break;

                case "esquerda":
                    transform.position = op��o_esquerda.position;
                    op��oAtual = op��o_esquerda.GetComponent<Op��o>();

                    AtualizarDire��es();

                    moveuPara = "esquerda";
                    break;

                case "direita":
                    transform.position = op��o_direita.position;
                    op��oAtual = op��o_direita.GetComponent<Op��o>();

                    AtualizarDire��es();

                    moveuPara = "direita";
                    break;
            }
        }
        else
        {
            GetComponent<Image>().color = corInicial;
        }

        /*
        if (GameObject.FindGameObjectsWithTag("Op��o").Length > limiteDeOp��es)
        {
            podeMover = true;
        }
        else
        {
            podeMover = false;
        }
        */
    }

    void AtualizarDire��es()
    {
        op��o_cima = op��oAtual.op��o_cima;
        op��o_baixo = op��oAtual.op��o_baixo;
        op��o_esquerda = op��oAtual.op��o_esquerda;
        op��o_direita = op��oAtual.op��o_direita;
    }

    void Corre��oDePosicionamento_P2()
    {
        if (op��oAtual.name == "Mega Buster (�cone)")
        {
            transform.position = GameObject.Find("Mega Saber (�cone)").transform.position;
            op��oAtual = GameObject.Find("Mega Saber (�cone)").GetComponent<Op��o>();

            AtualizarDire��es();
        }
    }
}

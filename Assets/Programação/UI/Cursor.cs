using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Cursor : MonoBehaviour
{
    public int jogador = 1;
    public int limiteDeOpções;
    public bool podeMover;
    public Opção opçãoAtual;
    public GameObject confirmação_sfx;

    public RectTransform opção_cima;
    public RectTransform opção_baixo;
    public RectTransform opção_esquerda;
    public RectTransform opção_direita;

    bool moveu;
    string moveuPara;

    Color corInicial;

    void Awake()
    {
        corInicial = GetComponent<Image>().color;

        AtualizarDireções();
    }

    void Update()
    {
        if (jogador == 2)
        {
            CorreçãoDePosicionamento_P2();
        }

        if (podeMover)
        {
            switch (jogador)
            {
                case 1:
                    if (Input.GetKeyDown(KeyCode.W) && opção_cima != null)
                    {
                        transform.position = opção_cima.position;
                        opçãoAtual = opção_cima.GetComponent<Opção>();

                        AtualizarDireções();

                        moveuPara = "cima";
                    }
                    if (Input.GetKeyDown(KeyCode.S) && opção_baixo != null)
                    {
                        transform.position = opção_baixo.position;
                        opçãoAtual = opção_baixo.GetComponent<Opção>();

                        AtualizarDireções();

                        moveuPara = "baixo";
                    }
                    if (Input.GetKeyDown(KeyCode.A) && opção_esquerda != null)
                    {
                        transform.position = opção_esquerda.position;
                        opçãoAtual = opção_esquerda.GetComponent<Opção>();

                        AtualizarDireções();

                        moveuPara = "esquerda";
                    }
                    if (Input.GetKeyDown(KeyCode.D) && opção_direita != null)
                    {
                        transform.position = opção_direita.position;
                        opçãoAtual = opção_direita.GetComponent<Opção>();

                        AtualizarDireções();

                        moveuPara = "direita";
                    }

                    if (TesteDeControles.joystickHabilitado)
                    {
                        if (Input.GetAxis("Vertical") > 0.5f && opção_cima != null && moveu == false)
                        {
                            transform.position = opção_cima.position;
                            opçãoAtual = opção_cima.GetComponent<Opção>();

                            AtualizarDireções();

                            moveuPara = "cima";
                            moveu = true;
                        }
                        if (Input.GetAxis("Vertical") < -0.5f && opção_baixo != null && moveu == false)
                        {
                            transform.position = opção_baixo.position;
                            opçãoAtual = opção_baixo.GetComponent<Opção>();

                            AtualizarDireções();

                            moveuPara = "baixo";
                            moveu = true;
                        }
                        if (Input.GetAxis("Horizontal") < -0.5f && opção_esquerda != null && moveu == false)
                        {
                            transform.position = opção_esquerda.position;
                            opçãoAtual = opção_esquerda.GetComponent<Opção>();

                            AtualizarDireções();

                            moveuPara = "esquerda";
                            moveu = true;
                        }
                        if (Input.GetAxis("Horizontal") > 0.5f && opção_direita != null && moveu == false)
                        {
                            transform.position = opção_direita.position;
                            opçãoAtual = opção_direita.GetComponent<Opção>();

                            AtualizarDireções();

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
                    if (Input.GetKeyDown(KeyCode.UpArrow) && opção_cima != null)
                    {
                        transform.position = opção_cima.position;
                        opçãoAtual = opção_cima.GetComponent<Opção>();

                        AtualizarDireções();

                        moveuPara = "cima";
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow) && opção_baixo != null)
                    {
                        transform.position = opção_baixo.position;
                        opçãoAtual = opção_baixo.GetComponent<Opção>();

                        AtualizarDireções();

                        moveuPara = "baixo";
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && opção_esquerda != null)
                    {
                        transform.position = opção_esquerda.position;
                        opçãoAtual = opção_esquerda.GetComponent<Opção>();

                        AtualizarDireções();

                        moveuPara = "esquerda";
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow) && opção_direita != null)
                    {
                        transform.position = opção_direita.position;
                        opçãoAtual = opção_direita.GetComponent<Opção>();

                        AtualizarDireções();

                        moveuPara = "direita";
                    }

                    if (TesteDeControles.joystickHabilitado)
                    {
                        if (Input.GetAxis("Vertical 2") > 0.5f && opção_cima != null && moveu == false)
                        {
                            transform.position = opção_cima.position;
                            opçãoAtual = opção_cima.GetComponent<Opção>();

                            AtualizarDireções();

                            moveuPara = "cima";
                            moveu = true;
                        }
                        if (Input.GetAxis("Vertical 2") < -0.5f && opção_baixo != null && moveu == false)
                        {
                            transform.position = opção_baixo.position;
                            opçãoAtual = opção_baixo.GetComponent<Opção>();

                            AtualizarDireções();

                            moveuPara = "baixo";
                            moveu = true;
                        }
                        if (Input.GetAxis("Horizontal 2") < -0.5f && opção_esquerda != null && moveu == false)
                        {
                            transform.position = opção_esquerda.position;
                            opçãoAtual = opção_esquerda.GetComponent<Opção>();

                            AtualizarDireções();

                            moveuPara = "esquerda";
                            moveu = true;
                        }
                        if (Input.GetAxis("Horizontal 2") > 0.5f && opção_direita != null && moveu == false)
                        {
                            transform.position = opção_direita.position;
                            opçãoAtual = opção_direita.GetComponent<Opção>();

                            AtualizarDireções();

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
                if (Input.GetKeyDown(KeyCode.G) && opçãoAtual.opçãoBloqueada == false)
                {
                    if (confirmação_sfx != null)
                    {
                        Instantiate(confirmação_sfx);
                    }
                    opçãoAtual.evento.Invoke();
                }

                if (TesteDeControles.joystickHabilitado && Input.GetButtonDown("Sul")
                    && opçãoAtual.opçãoBloqueada == false)
                {
                    if (confirmação_sfx != null)
                    {
                        Instantiate(confirmação_sfx);
                    }
                    opçãoAtual.evento.Invoke();
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.J) && opçãoAtual.opçãoBloqueada == false)
                {
                    if (confirmação_sfx != null)
                    {
                        Instantiate(confirmação_sfx);
                    }
                    opçãoAtual.evento_J2.Invoke();
                }

                if (TesteDeControles.joystickHabilitado && Input.GetButtonDown("Sul 2")
                    && opçãoAtual.opçãoBloqueada == false)
                {
                    if (confirmação_sfx != null)
                    {
                        Instantiate(confirmação_sfx);
                    }
                    opçãoAtual.evento.Invoke();
                }
                break;
        }

        if (opçãoAtual.opçãoBloqueada)
        {
            GetComponent<Image>().color = Color.clear;

            switch (moveuPara)
            {
                case "cima":
                    transform.position = opção_cima.position;
                    opçãoAtual = opção_cima.GetComponent<Opção>();

                    AtualizarDireções();

                    moveuPara = "cima";
                    break;

                case "baixo":
                    transform.position = opção_baixo.position;
                    opçãoAtual = opção_baixo.GetComponent<Opção>();

                    AtualizarDireções();

                    moveuPara = "baixo";
                    break;

                case "esquerda":
                    transform.position = opção_esquerda.position;
                    opçãoAtual = opção_esquerda.GetComponent<Opção>();

                    AtualizarDireções();

                    moveuPara = "esquerda";
                    break;

                case "direita":
                    transform.position = opção_direita.position;
                    opçãoAtual = opção_direita.GetComponent<Opção>();

                    AtualizarDireções();

                    moveuPara = "direita";
                    break;
            }
        }
        else
        {
            GetComponent<Image>().color = corInicial;
        }

        /*
        if (GameObject.FindGameObjectsWithTag("Opção").Length > limiteDeOpções)
        {
            podeMover = true;
        }
        else
        {
            podeMover = false;
        }
        */
    }

    void AtualizarDireções()
    {
        opção_cima = opçãoAtual.opção_cima;
        opção_baixo = opçãoAtual.opção_baixo;
        opção_esquerda = opçãoAtual.opção_esquerda;
        opção_direita = opçãoAtual.opção_direita;
    }

    void CorreçãoDePosicionamento_P2()
    {
        if (opçãoAtual.name == "Mega Buster (Ícone)")
        {
            transform.position = GameObject.Find("Mega Saber (Ícone)").transform.position;
            opçãoAtual = GameObject.Find("Mega Saber (Ícone)").GetComponent<Opção>();

            AtualizarDireções();
        }
    }
}

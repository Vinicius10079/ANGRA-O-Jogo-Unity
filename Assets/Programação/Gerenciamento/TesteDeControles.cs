using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteDeControles : MonoBehaviour
{
    public string[] nomesDosControles;

    public static bool joystickHabilitado = false;

    void Start()
    {
        nomesDosControles = Input.GetJoystickNames();

        for (int i = 0; i < nomesDosControles.Length; i++)
        {
            if (!string.IsNullOrEmpty(nomesDosControles[i]))
            {
                Debug.Log("Controle " + i + ": " + nomesDosControles[i]);
            }
        }
    }

    void Update()
    {
        // Verifica todos os botões do controle e exibe no console os que estão sendo pressionados
        for (int i = 0; i < 20; i++) // Assumindo um limite de 20 botões, ajuste conforme seu controle
        {
            if (Input.GetKey("joystick button " + i))
            {
                Debug.Log("Botão " + i + " pressionado");
            }
        }
    }

    void TesteDeControle1()
    {
        if (Input.GetButtonDown("Sul"))
        {
            print("Sul");
        }
        if (Input.GetButtonDown("Oeste"))
        {
            print("Oeste");
        }
        if (Input.GetButtonDown("Leste"))
        {
            print("Leste");
        }
        if (Input.GetButtonDown("Norte"))
        {
            print("Norte");
        }

        if (Input.GetButtonDown("OE"))
        {
            print("OE");
        }
        if (Input.GetButtonDown("OD"))
        {
            print("OD");
        }

        if (Input.GetButtonDown("GE"))
        {
            print("GE");
        }
        if (Input.GetButtonDown("GD"))
        {
            print("GD");
        }

        if (Input.GetButtonDown("Start"))
        {
            print("Start");
        }
        if (Input.GetButtonDown("Select"))
        {
            print("Select");
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.5f ||
            Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5f)
        {
            print(Input.GetAxis("Vertical"));
            print(Input.GetAxis("Horizontal"));
        }

    }

    void TesteDeControle2()
    {
        if (Input.GetButtonDown("Sul 2"))
        {
            print("Sul");
        }
        if (Input.GetButtonDown("Oeste 2"))
        {
            print("Oeste");
        }
        if (Input.GetButtonDown("Leste 2"))
        {
            print("Leste");
        }
        if (Input.GetButtonDown("Norte 2"))
        {
            print("Norte");
        }

        if (Input.GetButtonDown("OE 2"))
        {
            print("OE");
        }
        if (Input.GetButtonDown("OD 2"))
        {
            print("OD");
        }

        if (Input.GetButtonDown("GE 2"))
        {
            print("GE");
        }
        if (Input.GetButtonDown("GD 2"))
        {
            print("GD");
        }

        if (Input.GetButtonDown("Start 2"))
        {
            print("Start");
        }
        if (Input.GetButtonDown("Select 2"))
        {
            print("Select");
        }

        if (Mathf.Abs(Input.GetAxis("Vertical 2")) > 0.5f ||
            Mathf.Abs(Input.GetAxis("Horizontal 2")) > 0.5f)
        {
            print(Input.GetAxis("Vertical 2"));
            print(Input.GetAxis("Horizontal 2"));
        }

    }
}

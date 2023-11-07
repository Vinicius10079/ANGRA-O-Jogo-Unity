using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunçõesDeCâmera : MonoBehaviour
{
    [Header("Balançar Câmera")]
    public bool balançarCâmera;
    public float amplitude = 0.1f;
    public float velocidade = 1.0f;
    public float duração = 2.0f;
    public bool balançandoCâmera;

    Camera câmeraPrincipal;
    Vector3 posiçãoInicial;
    float tempoInicial;

    void Awake()
    {
        câmeraPrincipal = GetComponent<Camera>();
    }

    void Start()
    {
        AtualizarPosiçãoInicial();
    }

    void Update()
    {
        if (balançarCâmera)
        {
            balançarCâmera = false;
            StartCoroutine(BalançarCamera());
        }
    }

    public void AtualizarPosiçãoInicial()
    {
        posiçãoInicial = câmeraPrincipal.transform.position;
    }

    IEnumerator BalançarCamera()
    {
        balançandoCâmera = true;
        AtualizarPosiçãoInicial();

        tempoInicial = Time.time;

        while (Time.time - tempoInicial < duração)
        {
            float bordaX = amplitude * Mathf.Sin(velocidade * (Time.time - tempoInicial));
            câmeraPrincipal.transform.position = posiçãoInicial + new Vector3(bordaX, 0, 0);
            yield return null;
        }

        câmeraPrincipal.transform.position = posiçãoInicial;
        balançandoCâmera = false;
    }
}

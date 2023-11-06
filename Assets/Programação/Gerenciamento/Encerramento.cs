using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encerramento : MonoBehaviour
{
    public float velocidade = 5f;

    void Update()
    {
        transform.Translate(Vector3.up * velocidade * Time.deltaTime);
    }
}

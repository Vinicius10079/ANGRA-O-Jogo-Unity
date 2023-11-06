using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machado_Cavaleiro : MonoBehaviour
{
    public float velocidade = 10f;

    void Update()
    {
        transform.Rotate(Vector3.forward, velocidade * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosão_ExplosãoCircular : MonoBehaviour
{
    public float velocidade = 5;

    void Update()
    {
        transform.Translate(Vector2.up * velocidade * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

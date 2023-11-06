using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefe_Ataque1 : MonoBehaviour
{
    public float dura��o = 4f;
    public float velocidade;

    Transform alvo;
    GameObject[] jog;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Ataque (Chefe)").Length > 0)
        {
            Destroy(gameObject);
        }
        jog = GameObject.FindGameObjectsWithTag("Player");
        alvo = jog[Random.Range(0, jog.Length)].transform;

        Invoke("Destruir", dura��o);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidade * Time.deltaTime);
    }

    void Destruir()
    {
        Destroy(gameObject);
    }
}

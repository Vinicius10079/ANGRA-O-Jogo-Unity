using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefe_Ataque3 : MonoBehaviour
{
    public GameObject projétil;
    public GameObject disparo_sfx;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Ataque (Chefe)").Length > 0)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Disparar();
    }

    void Disparar()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(projétil, transform.position, transform.rotation);
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 45);
        }

        Destroy(gameObject);
    }
}

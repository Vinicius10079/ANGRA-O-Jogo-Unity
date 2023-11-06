using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontuação_Âncora : MonoBehaviour
{
    GameObject filho;

    void Awake()
    {
        filho = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (filho == null)
        {
            Destroy(gameObject);
        }
    }
}

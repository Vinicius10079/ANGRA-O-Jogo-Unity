using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuação : MonoBehaviour
{
    public int pontuação;

    Text texto;

    void Awake()
    {
        texto = GetComponent<Text>();
    }

    void Update()
    {
        texto.text = pontuação.ToString();

        if (pontuação < 0)
        {
            pontuação = 0;
        }
    }
}

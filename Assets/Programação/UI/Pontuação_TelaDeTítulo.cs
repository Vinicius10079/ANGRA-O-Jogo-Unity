using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuação_TelaDeTítulo : MonoBehaviour
{
    public GameObject[] textos;

    void Awake()
    {
        if (PlayerPrefs.GetInt("Pontuacao 1") != 0)
        {
            textos[0].GetComponent<Text>().text = PlayerPrefs.GetInt("Pontuacao 1") + "";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setor : MonoBehaviour
{
    public int checkpoint;
    public GameObject checkpoint_Texto;
    public Vector2 inimigosDestruidos;

    void Start()
    {

        if (checkpoint_Texto != null && PlayerPrefs.GetInt("checado") == 0)
        {
            PlayerPrefs.SetInt("checado", checkpoint);
            checkpoint_Texto.SetActive(true);

            Invoke("DesativarCheckpoint_Texto", 1.6f);
        }
    }

    void DesativarCheckpoint_Texto()
    {
        checkpoint_Texto.SetActive(false);
    }
}

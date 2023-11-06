using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDeHomenagem : MonoBehaviour
{
    public void MudarDeCena()
    {
        SceneManager.LoadScene("Tela de Título");
    }
}

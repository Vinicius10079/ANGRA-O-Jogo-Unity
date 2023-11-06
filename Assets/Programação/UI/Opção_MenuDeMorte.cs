using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opção_MenuDeMorte : MonoBehaviour
{
    public GerenciadorDeFase gf;

    void Awake()
    {
        gf.podePausar = false;
    }

    public void DesativarMenu()
    {
        gf.menuDeMorte.SetActive(false);
    }
}

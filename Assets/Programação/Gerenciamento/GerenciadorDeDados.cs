using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeDados : MonoBehaviour
{
    public static void Salvar_GD(string chave, string tipodeDado, int valor_Int, float valor_Float, string valor_String)
    {
        switch (tipodeDado)
        {
            case "int":
                PlayerPrefs.SetInt(chave, valor_Int);
                break;

            case "float":
                PlayerPrefs.SetFloat(chave, valor_Float);
                break;

            case "string":
                PlayerPrefs.SetString(chave, valor_String);
                break;
        }
    }

    public static int CarregarInt_GD(string chave)
    {
        return PlayerPrefs.GetInt(chave);
    }
    public static float CarregarFloat_GD(string chave)
    {
        return PlayerPrefs.GetFloat(chave);
    }
    public static string CarregarString_GD(string chave)
    {
        return PlayerPrefs.GetString(chave);
    }
}

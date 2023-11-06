using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Opção : MonoBehaviour
{
    public bool opçãoBloqueada;
    public RectTransform opção_cima;
    public RectTransform opção_baixo;
    public RectTransform opção_esquerda;
    public RectTransform opção_direita;

    public UnityEvent evento;
    public UnityEvent evento_J2;
}

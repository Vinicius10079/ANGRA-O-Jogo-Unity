using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorDeCombo : MonoBehaviour
{
    public void Acrescentar()
    {
        FindObjectOfType<Combo_Texto>().combo++;
    }
}

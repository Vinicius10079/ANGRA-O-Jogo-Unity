using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relâmpago : MonoBehaviour
{
    public GameObject trovão;

    public void InstanciarTrovão()
    {
        Instantiate(trovão);
    }
}

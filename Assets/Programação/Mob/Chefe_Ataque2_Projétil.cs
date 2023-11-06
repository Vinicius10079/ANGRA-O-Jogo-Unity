using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefe_Ataque2_Projétil : MonoBehaviour
{
    public float delay;
    public GameObject sfx;

    void Start()
    {
        Invoke("AtivarProjétil", delay);
    }

    void AtivarProjétil()
    {
        if (sfx != null)
        {
            Instantiate(sfx);
        }

        GetComponent<Projétil>().enabled = true;
    }
}

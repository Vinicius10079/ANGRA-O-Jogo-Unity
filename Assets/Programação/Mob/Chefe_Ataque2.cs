using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefe_Ataque2 : MonoBehaviour
{
    public float delay = 1f;
    public GameObject[] projÚteis;

    float posX;

    Transform cam;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Ataque (Chefe)").Length > 0)
        {
            Destroy(gameObject);
        }
        posX = transform.position.x;
        cam = FindObjectOfType<Camera>().transform;

        transform.position = new Vector3(cam.position.x, cam.position.y + 8.5f);

        foreach (GameObject projÚtil in projÚteis)
        {
            projÚtil.transform.parent = null;
            projÚtil.SetActive(true);
        }

        Destroy(gameObject);
    }
}

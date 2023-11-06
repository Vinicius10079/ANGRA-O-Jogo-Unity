using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefe_Ataque5 : MonoBehaviour
{
    public GameObject projétil;
    public float intervalo = 1f;
    public GameObject sfx;

    int disparos = 0;

    GameObject[] jogadores;
    Transform jogador;
    Camera cam;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Ataque (Chefe)").Length > 0)
        {
            Destroy(gameObject);
        }
        jogadores = GameObject.FindGameObjectsWithTag("Player");
        jogador = jogadores[Random.Range(0, jogadores.Length)].transform;
        cam = FindObjectOfType<Camera>();
    }

    void Start()
    {
        if (jogador.position.x >= transform.position.x)
        {
            transform.position = new Vector3(cam.transform.position.x - 12, transform.position.y);
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.position = new Vector3(cam.transform.position.x + 12, transform.position.y);
            transform.eulerAngles = new Vector3(0, 180);
        }

        StartCoroutine(DisparosMultiplos());
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, jogador.position.y);
    }

    IEnumerator DisparosMultiplos()
    {
        while (disparos <= 3)
        {
            Instantiate(projétil, transform.position, transform.rotation);
            disparos++;
            if (sfx != null)
            {
                Instantiate(sfx);
            }

            if (disparos > 3)
            {
                print("Ataque 3 fim");
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(intervalo);
        }
    }
}

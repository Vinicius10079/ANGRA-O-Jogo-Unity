using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosãoCircular : MonoBehaviour
{
    public GameObject[] explosões;

    void Start()
    {
        transform.eulerAngles = Vector2.zero;

        foreach (GameObject explosão in explosões)
        {
            Instantiate(explosão, transform.position, transform.rotation);

            //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 45);
        }

        Destroy(gameObject);
    }
}

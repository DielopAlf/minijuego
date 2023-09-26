using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovFondo : MonoBehaviour
{
    // Start is called before the first frame update

    public float Velocidad;
    public GameObject Objeto;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -10)
        {
            transform.position = Objeto.transform.position + new Vector3(0, 10, 0);

        }

        transform.position += new Vector3(0, Velocidad * Time.deltaTime, 0);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float Velocidad;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0, Velocidad * Time.deltaTime, 0);

    }
    private void OncollicionEnter2D (Collision2D collision)
    {

        if(collision.gameObject.name == "enemy")
        {

            collision.gameObject.GetComponentInParent<NaveEnemiga>().Vida -= 1;

        }

    }



}

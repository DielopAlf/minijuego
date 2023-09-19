using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavePrincipal : MonoBehaviour
{
    [SerializeField]
    KeyCode botonDerecha;
    [SerializeField]
    KeyCode botonIzquierda;
    [SerializeField]
    public float Speed = 10f;
    public float maxdistancia;
    //public PrincipalShip nave;
    



    Rigidbody player;



    void Update()
    {

        if (Input.GetKey(botonDerecha) && gameObject.transform.position.x < maxdistancia)
        {


            transform.position += Vector3.right * Time.deltaTime * Speed;

        }

        if (Input.GetKey(botonIzquierda) && gameObject.transform.position.x > maxdistancia * -1)
        {

            transform.position += Vector3.left * Time.deltaTime * Speed;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton_puerta : MonoBehaviour
{

    public Puerta[] puertas_asociadas_1;
    public Puerta[] puertas_asociadas_2;

    public bool estado_1;
    public bool interact;

    // Start is called before the first frame update
    void Start()
    {
        interact = false;

        if(estado_1)
        {
            foreach (Puerta p1 in puertas_asociadas_1)
            {
                p1.AbrirPuerta();

            }
            foreach (Puerta p2 in puertas_asociadas_2)
            {
                p2.CerrarPuerta();

            }
        }
        else
        {
            foreach (Puerta p2 in puertas_asociadas_2)
            {
                p2.AbrirPuerta();

            }
            foreach (Puerta p1 in puertas_asociadas_1)
            {
                p1.CerrarPuerta();

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("x") && interact)
        {
            Debug.Log("Abriendo puertas");
            cambio_estado();
        }
    }

    public void cambio_estado()
    {
        if(estado_1)
        {
            foreach(Puerta p1 in puertas_asociadas_1)
            {
                p1.CerrarPuerta();
                
            }
            foreach (Puerta p2 in puertas_asociadas_2)
            {
                p2.AbrirPuerta();

            }
            estado_1 = false;
        }

        else
        {
            foreach (Puerta p2 in puertas_asociadas_2)
            {
                p2.CerrarPuerta();

            }
            foreach (Puerta p1 in puertas_asociadas_1)
            {
                p1.AbrirPuerta();

            }
            estado_1 = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Entrando en computadora puertas");
            interact = true;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Saliendo de computadora puertas");
            interact = false;

        }
    }


}

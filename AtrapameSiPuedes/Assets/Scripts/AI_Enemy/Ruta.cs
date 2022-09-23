using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruta : MonoBehaviour
{

    public Transform[] puntos_ruta;

    private int punto_actual = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pasar_punto()
    {
        punto_actual++;
        if (punto_actual == puntos_ruta.Length)
            punto_actual = 0;
    }

    public void cambiar_objetivo()
    {

    }

}

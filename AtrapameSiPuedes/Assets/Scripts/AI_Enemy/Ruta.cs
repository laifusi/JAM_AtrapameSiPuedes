using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ruta
{
    public Transform[] puntos_ruta;

    private int punto_actual = 0;

    public void pasar_punto()
    {
        punto_actual++;
        if (punto_actual == puntos_ruta.Length)
            punto_actual = 0;
    }

    public Vector3 getPuntoActual()
    {
        return puntos_ruta[punto_actual].position;
    }
}

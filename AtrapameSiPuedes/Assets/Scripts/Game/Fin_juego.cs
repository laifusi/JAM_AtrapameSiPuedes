using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin_juego : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Derrota
            Game_Controller.Instance.Derrota();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Derrota
            Debug.Log("Cogido");
            gc.Derrota();
        }
    }
}

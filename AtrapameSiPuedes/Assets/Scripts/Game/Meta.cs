using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Jugador detectado");
            if (other.gameObject.GetComponent<Character_Controller>().objeto_robado)
            {
                //Victoria
                Game_Controller.Instance.Victoria();
            }
        }
    }
}

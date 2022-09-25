using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fin_juego : MonoBehaviour
{
    public Game_Controller gc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("entro en collider");
        if (collision.gameObject.tag == "Player")
        {
            //Derrota
            Debug.Log("Cogido");
            Game_Controller.Instance.Derrota();
        }
    }
}

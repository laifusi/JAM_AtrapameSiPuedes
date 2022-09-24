using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{

    public GameObject puerta_render;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirPuerta()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        puerta_render.SetActive(false);
    }

    public void CerrarPuerta()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        puerta_render.SetActive(true);
    }
}

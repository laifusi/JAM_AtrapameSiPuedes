using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Imagen : MonoBehaviour
{
    public Sprite vaina_activa;
    public Sprite vaina_oculta;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = vaina_oculta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cambiar_imagen_activa()
    {
        this.GetComponent<Image>().sprite = vaina_activa;
    }

    public void Cambiar_imagen_oculta()
    {
        this.GetComponent<Image>().sprite = vaina_oculta;
    }
}

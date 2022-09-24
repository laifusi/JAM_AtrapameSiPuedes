using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Salir_del_Juego()
    {
        Application.Quit();
    }


    public void Comenzar_a_Jugar()
    {
        SceneManager.LoadScene("Nivel_1");
    }

    public void Menu_Principal()
    {
        SceneManager.LoadScene("Inicia");
    }

    public void Derrota()
    {
        SceneManager.LoadScene("Derrota");
    }

    public void Victoria()
    {
        SceneManager.LoadScene("Victoria");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaina_transporte : MonoBehaviour
{

    Vector3 position_vaina;
    GameObject character;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void create_vaina(Vector3 position, GameObject personaje)
    {
        this.position_vaina = position;
        character = personaje;
    }
}

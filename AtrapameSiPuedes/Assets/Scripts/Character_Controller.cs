using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 5.0f;
    public GameObject vaina;
    private List<GameObject> lista_vainas;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        lista_vainas = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        poner_vaina();

        
    }

    public void poner_vaina()
    {
        if(Input.GetKeyDown("e"))
        {
            if(lista_vainas.Count == 0)
            {
                GameObject v = Instantiate(vaina, this.transform.position, Quaternion.identity);
                v.GetComponent<Vaina_transporte>().create_vaina(this.transform.position, this.gameObject);
                lista_vainas.Add(v);
            }

            else
            {
                GameObject v = lista_vainas[0];
                v.GetComponent<Vaina_transporte>().teletransporte();
                lista_vainas.Remove(v);
                Destroy(v);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

}

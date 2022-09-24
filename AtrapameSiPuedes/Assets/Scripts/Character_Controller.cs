using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 5f;
    public GameObject vaina;
    public GameObject vaina_1, vaina_2;
    public bool objeto_robado;
    public Imagen ico_vaina_1, ico_vaina_2;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

        vaina_1 = null;
        vaina_2 = null;

        objeto_robado = false;
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
            if(vaina_1 == null)
            {
                GameObject v = Instantiate(vaina, this.transform.position, Quaternion.identity);
                v.GetComponent<Vaina_transporte>().create_vaina(this.transform.position, this.gameObject);
                vaina_1 = v;
                ico_vaina_1.Cambiar_imagen_activa();
            }

            else
            {
                teletransporte(vaina_1.transform.position);
                Destroy(vaina_1);
                ico_vaina_1.Cambiar_imagen_oculta();
            }
        }

        if (Input.GetKeyDown("q"))
        {
            if (vaina_2 == null)
            {
                GameObject v = Instantiate(vaina, this.transform.position, Quaternion.identity);
                v.GetComponent<Vaina_transporte>().create_vaina(this.transform.position, this.gameObject);
                vaina_2 = v;
                ico_vaina_2.Cambiar_imagen_activa();
            }

            else
            {
                teletransporte(vaina_2.transform.position);
                Destroy(vaina_2);
                ico_vaina_2.Cambiar_imagen_oculta();
            }
        }
    }

    public void teletransporte(Vector3 position_vaina)
    {
        controller.enabled = false;
        this.gameObject.transform.position = position_vaina;
        controller.enabled = true;
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Prize")
        {
            objeto_robado = true;
            Destroy(other.gameObject);
        }
    }

}

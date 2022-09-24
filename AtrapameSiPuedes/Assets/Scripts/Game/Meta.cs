using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Character_Controller>().objeto_robado)
            {
                //Victoria
                Game_Controller.Instance.Victoria();
            }
        }
    }
}

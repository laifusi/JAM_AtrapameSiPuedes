using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetiveObject : MonoBehaviour
{
    [SerializeField] private FSMController[] agentsToAlert;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Character_Controller>().objeto_robado = true;
            foreach (FSMController agent in agentsToAlert)
            {
                agent.Alarma(transform.position);
            }
        }
    }
}

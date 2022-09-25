using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetiveObject : MonoBehaviour
{
    [SerializeField] private FSMController[] agentsToAlert;

    [SerializeField] private GameObject pared_invisible;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Character_Controller>().objeto_robado = true;
            pared_invisible.SetActive(true);
            foreach (FSMController agent in agentsToAlert)
            {
                agent.Alarma(transform.position);
            }

            MusicManager.Instance.PlayObjectiveClip();

            Destroy(this.gameObject);
        }
    }
}

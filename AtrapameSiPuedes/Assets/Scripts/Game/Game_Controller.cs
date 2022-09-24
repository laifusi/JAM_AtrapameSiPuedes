using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{

    public AudioSource reproductor_audio;

    public bool alarma;
    public GameObject panel_alarma;

    // Start is called before the first frame update
    void Start()
    {
        alarma = true;
        desactivarAlarma();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Victoria()
    {
        Debug.Log("Ganas la partida");
    }

    public void Derrota()
    {
        Debug.Log("Pierdes la partida");
    }

    public void activarAlarma()
    {
        reproductor_audio.Play();
        panel_alarma.SetActive(true);
    }

    public void desactivarAlarma()
    {
        reproductor_audio.Stop();
        panel_alarma.SetActive(false);
    }
}

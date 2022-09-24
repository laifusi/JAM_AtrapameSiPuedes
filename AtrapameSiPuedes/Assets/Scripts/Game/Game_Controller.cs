using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller Instance;

    public AudioSource reproductor_audio;

    public GameObject panel_alarma;

    private int alertedElements;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        desactivarAlarma();
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
        if(!reproductor_audio.isPlaying)
            reproductor_audio.Play();
        panel_alarma.SetActive(true);
    }

    public void desactivarAlarma()
    {
        reproductor_audio.Stop();
        panel_alarma.SetActive(false);
    }
}

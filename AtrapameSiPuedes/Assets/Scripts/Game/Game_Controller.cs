using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller Instance;

    public AudioSource reproductor_audio;

    public GameObject panel_alarma;

    public Menus menu;

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
        Physics.IgnoreLayerCollision(7, 8);
    }

    public void Victoria()
    {
        Debug.Log("Ganas la partida");
        MusicManager.Instance.PlayWinClip();
        menu.Victoria();
    }

    public void Derrota()
    {
        Debug.Log("Pierdes la partida");
        MusicManager.Instance.PlayLoseClip();
        menu.Derrota();
    }

    public void activarAlarma()
    {
        if(!reproductor_audio.isPlaying)
        {
            reproductor_audio.Play();
            MusicManager.Instance.ChangeToAlert();
        }
        panel_alarma.SetActive(true);
    }

    public void desactivarAlarma()
    {
        reproductor_audio.Stop();
        MusicManager.Instance.ChangeToStealth();
        panel_alarma.SetActive(false);
    }
}

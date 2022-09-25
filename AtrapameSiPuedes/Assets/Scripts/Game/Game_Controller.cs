using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller Instance;

    public AudioSource reproductor_audio;

    public GameObject panel_alarma;

    public Menus menu;

    public string[] indicaciones = new string[4];

    public GameObject panel_text;

    public TMP_Text text;

    public float timer = 6;

    public float timer_actual = 4;

    private int texto_actual = 0;

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

    private void Update()
    {
        timer_actual += Time.deltaTime;
        if(timer_actual > timer)
        {
            if(texto_actual < indicaciones.Length)
            {
                panel_text.SetActive(true);
                Cambiar_Texto(texto_actual);
                texto_actual++;
            }

            else
            {
                panel_text.SetActive(false);
            }
            timer_actual = 0;
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
        desactivarAlarma();
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

    public void Cambiar_Texto(int i)
    {
        text.text = indicaciones[i];
    }
}

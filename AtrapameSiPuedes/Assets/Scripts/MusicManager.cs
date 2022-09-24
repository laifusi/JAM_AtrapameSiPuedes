using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip stealth;
    [SerializeField] private AudioClip alert;

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

        audioSource = GetComponent<AudioSource>();
        ChangeToStealth();
    }

    public void ChangeToStealth()
    {
        audioSource.clip = stealth;
        audioSource.Play();
    }

    public void ChangeToAlert()
    {
        audioSource.clip = alert;
        audioSource.Play();
    }
}

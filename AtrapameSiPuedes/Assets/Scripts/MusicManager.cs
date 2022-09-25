using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip stealth;
    [SerializeField] private AudioClip alert;

    [Header("Win Or Lose Sound Effects")]
    [SerializeField] AudioSource winOrLoseAS;
    [SerializeField] private AudioClip objectiveClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;

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

    public void PlayObjectiveClip()
    {
        winOrLoseAS.PlayOneShot(objectiveClip);
    }

    public void PlayWinClip()
    {
        winOrLoseAS.PlayOneShot(winClip);
    }


    public void PlayLoseClip()
    {
        winOrLoseAS.PlayOneShot(loseClip);
    }
}

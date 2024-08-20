using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] expunge;
    public AudioClip[] fall;
    public AudioClip[] pickups;
    public AudioClip[] changeStage;
    public AudioClip[] music;

    [Range(0, 1)] public float m_Volume;

    public AudioSource sfxSource;
    public AudioSource musicSource;


    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PlayMusic();
    }

    public void PlayPickup()
    {
        if (sfxSource == null)
        {
            return;
        }
        int randIndex = Random.Range(0, pickups.Length);
        sfxSource.PlayOneShot(pickups[randIndex]);
    }

    public void PlayExpunge()
    {
        if (sfxSource == null)
        {
            return;
        }

        int randIndex = Random.Range(0, expunge.Length);
        sfxSource.PlayOneShot(expunge[randIndex]);
    }
    public void PlayFall()
    {
        if (sfxSource == null)
        {
            return;
        }

        int randIndex = Random.Range(0, fall.Length);
        sfxSource.PlayOneShot(fall[randIndex]);
    }

    public void PlayChangeState(int state)
    {
        if (sfxSource == null)
        {
            return;
        }

        sfxSource.PlayOneShot(changeStage[state]);
    }


    public void PlayMusic()
    {
        if (musicSource == null)
        {
            return;
        }
        musicSource.loop = true;

        musicSource.PlayOneShot(music[0]);
    }
}

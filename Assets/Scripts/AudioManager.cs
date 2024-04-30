using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gm = new GameObject("AudioManager");
                return gm.AddComponent<AudioManager>();
            }
            return instance;
        }
        set
        {
            if (instance == null)
            {
                instance = value;
                DontDestroyOnLoad(value.gameObject);
            }
            else
            {
                Destroy(value.gameObject);
            }
        }
    }
    private static AudioManager instance;

    public List<Sound> musicSounds = new(), sfxSounds = new();
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if(musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        PlayMusic("Theme");
    }

    public void MuteSFX()
    {
        sfxSource.Stop();
    }
    public void PlayMusic(string name)
    {
        Sound s = musicSounds.Find(x => x.name == name);

        if (s == null)
        {
            Debug.Log("«вук не найден");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = sfxSounds.Find(x => x.name == name);

        if (s == null)
        {
            Debug.Log("«вук не найден");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
}

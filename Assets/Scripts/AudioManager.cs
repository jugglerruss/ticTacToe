using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic; 
    [SerializeField] private AudioSource _popSource; 
    [SerializeField] private AudioClip[] _popSounds; 
    [SerializeField] private AudioClip[] _popFailSounds; 
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(AudioManager).Name;
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    public void PlayMusic()
    {
        _backgroundMusic.Play();
    }
    public void PlayPop()
    {
        _popSource.pitch = 1f;
        _popSource.clip = _popSounds[Random.Range(0, _popSounds.Length)];
        _popSource.Play();
    }
    public void PlayFailPop()
    {
        _popSource.pitch = 0.5f;
        _popSource.clip = _popFailSounds[Random.Range(0, _popFailSounds.Length)];
        _popSource.Play();
    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic; 
    [SerializeField] private AudioSource _popSource; 
    [SerializeField] private AudioSource _portalSource; 
    [SerializeField] private AudioSource _UISource; 
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
        _popSource.pitch = Random.Range(1, 1.5f);
        _popSource.clip = _popSounds[Random.Range(0, _popSounds.Length)];
        _popSource.Play();
    }
    public void PlayFailPop()
    {
        _popSource.pitch = Random.Range(0.3f, 0.6f);
        _popSource.clip = _popFailSounds[Random.Range(0, _popFailSounds.Length)];
        _popSource.Play();
    }
    public void PlayPortal()
    {
        _portalSource.Play();
    }
    public void PlayUIclick()
    {
        _UISource.Play();
    }
    public void MuteMusic(bool mute)
    {
        if(mute)
            _backgroundMusic.Stop();
        else
            _backgroundMusic.Play();
    }
}

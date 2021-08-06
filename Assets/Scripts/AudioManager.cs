using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic; 
    [SerializeField] private AudioSource _popSource; 
    [SerializeField] private AudioSource _portalSource; 
    [SerializeField] private AudioSource _dragSource; 
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
    private void Awake()
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
    public void PlayPop(int counter)
    {
        _popSource.pitch = 1.1f - counter * 0.1f;
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
    public void PlayDrag()
    {
        if (_backgroundMusic.isPlaying)
        {
            _backgroundMusic.pitch = 2.5f;
            _backgroundMusic.volume = 1;
        }
            
    }
    public void StopDrag()
    {
        if (_backgroundMusic.isPlaying)
        {
            _backgroundMusic.pitch = 1;
            _backgroundMusic.volume = 0.5f;
        }
    }
    public void PlayUIclick()
    {
        _UISource.Play();
    }
    public void MuteMusic(bool mute)
    {
        PlayerPrefs.SetInt("mute", mute ? 1 : 0);
        if (mute)
            _backgroundMusic.Stop();
        else
            _backgroundMusic.Play();
    }
}

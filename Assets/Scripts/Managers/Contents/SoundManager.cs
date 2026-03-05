using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource _audioSource;

    void Awake()
    {
        if(Instance == null)
            Instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        if(clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}

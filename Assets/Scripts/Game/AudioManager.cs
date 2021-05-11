using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource audioSource;

    private void Awake()
    {
        var newAudioSource = new GameObject();
        newAudioSource.transform.SetParent(transform);

        audioSource = newAudioSource.AddComponent<AudioSource>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string soundName, bool shouldPlayInAudioSource = false)
    {
        var clip = Resources.Load<AudioClip>($"Sounds/{soundName}");

        if (shouldPlayInAudioSource)
        {
            PlayClipInAudioSource(clip);
            return;
        }
        
        PlayClip(clip);
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // This hack is used to still play audio while other scene is loading without
    // rudely stopping it
    public void PlayClipInAudioSource(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
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

    public void PlaySound(string soundName)
    {
        var clip = Resources.Load<AudioClip>($"Sounds/{soundName}");

        PlayClip(clip);
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
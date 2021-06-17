using UnityEngine;

public static class Sound
{
    public static void Play(string name) 
    {
        GameEvents.onAudioNamePlayRequested.Invoke(name);
    }

    public static void Play(AudioClip clip)
    {
        GameEvents.onAudioClipPlayRequested.Invoke(clip);
    }
}

using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Test = 0,
    None,
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    // Singleton
    public static AudioManager Instace = null;

    [SerializeField] private AudioSource _audioSource = null;

    // Resources of audio
    [SerializeField]
    private List<AudioKeyValuePair> _audioList;

    [System.Serializable]
    private class AudioKeyValuePair
    {
        public AudioType key;
        public AudioClip value;
    }

    private Dictionary<AudioType, AudioClip> _audioDictionary = new();

    private void Awake()
    {
        if (Instace == null)
        {
            Instace = this;
            DontDestroyOnLoad(gameObject);

            foreach (var au in _audioList)
                _audioDictionary.Add(au.key, au.value);
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = false;
    }

    public void PlayerSFX(AudioType type)
    {
        _audioSource.PlayOneShot(_audioDictionary[type]);
    }

    public void PlayerSFX(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void MuteSFX()
    {

        _audioSource.volume = 0.0f;

    }

    public void UnMuteSFX()
    {

        _audioSource.volume = 1.0f;

    }
}

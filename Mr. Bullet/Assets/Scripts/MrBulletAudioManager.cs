using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBulletAudioManager : MonoBehaviour
{
    public static MrBulletAudioManager mrBulletAM;

    private AudioSource _audioSource;

    void Awake()
    {
        mrBulletAM = this;
         
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlaySoundFX(AudioClip clip, float volume)
    {
        _audioSource.PlayOneShot(clip, volume);
    }
}

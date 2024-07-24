using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().volume = AudioMaster.SFXVolume;
        Destroy(gameObject, clip.length +0.1f);
    }
    
}

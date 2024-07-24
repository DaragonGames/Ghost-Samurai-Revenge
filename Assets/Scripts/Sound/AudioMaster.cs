using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public static float SFXVolume = 0.7f;
    public static float MusicVolume = 1;
    public AudioSource bossSound;
    public AudioSource levelSound;
    public AudioSource ghostSound;
    public AudioSource[] hpSound;
    private bool boss;
    private int hpSoundPlaying = 0;

    void Start()
    {
        bossSound.PlayScheduled(AudioSettings.dspTime + 2);
        levelSound.PlayScheduled(AudioSettings.dspTime + 2);
        ghostSound.PlayScheduled(AudioSettings.dspTime + 2);
        hpSound[0].PlayScheduled(AudioSettings.dspTime + 2);
        hpSound[1].PlayScheduled(AudioSettings.dspTime + 2);
        hpSound[2].PlayScheduled(AudioSettings.dspTime + 2);
    }

    void Update()
    {        
        if (GameManager.Instance.gameState == GameManager.GameState.Boss)
        {
            EnterBossFight();
            boss = true;
        }
        float hpPercentage = GameManager.Instance.player.GetComponent<Player>().GetHealthPercentage();
        if (hpPercentage > 0.6f && hpSoundPlaying != 0)
        {
            StartCoroutine(FadeInOrOut(hpSound[0], 1, MusicVolume*0.7f));
            StartCoroutine(FadeInOrOut(hpSound[1], 1, 0));
            StartCoroutine(FadeInOrOut(hpSound[2], 1, 0));
            hpSoundPlaying = 0;            
        }
        if (hpPercentage < 0.3f && hpSoundPlaying != 2)
        {
            StartCoroutine(FadeInOrOut(hpSound[0], 1, 0));
            StartCoroutine(FadeInOrOut(hpSound[1], 1, 0));
            StartCoroutine(FadeInOrOut(hpSound[2], 1, MusicVolume*0.7f));
            hpSoundPlaying = 2;            
        }
        if (hpPercentage > 0.3f && hpPercentage < 0.6f &&  hpSoundPlaying != 1)
        {
            StartCoroutine(FadeInOrOut(hpSound[0], 1, 0));
            StartCoroutine(FadeInOrOut(hpSound[1], 1, MusicVolume*0.7f));
            StartCoroutine(FadeInOrOut(hpSound[2], 1, 0));
            hpSoundPlaying = 1;            
        }
    }

    void EnterBossFight()
    {
        if (!boss)
        {
            StartCoroutine(FadeInOrOut(bossSound, 4, MusicVolume));
            StartCoroutine(FadeInOrOut(levelSound, 2, 0));
        }        
    }

    IEnumerator FadeInOrOut(AudioSource source, float duration, float targetVolume)
    {
        float steps = (targetVolume - source.volume) / 20;
        for (int i = 0; i <20; i++)
        {
            source.volume += steps;
            yield return new WaitForSeconds(duration/20f);
        }        
    }
    
}

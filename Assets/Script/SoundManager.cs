using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // ΩÃ±€≈Ê∆–≈œ

    public AudioClip[] SoundGroup;
    public AudioClip[] EffectGroup;

    public AudioSource Bgm_Sound;
    public AudioSource Effect_Sound;
    public AudioSource Effect_Sound_2;
    public AudioSource Effect_Sound_3;

    public bool isboss = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SoundChange(int value)
    {
        switch (value)
        {
            case 0:
                Bgm_Sound.clip = SoundGroup[0];
                Bgm_Sound.Play();
                break;
            case 1:
                Bgm_Sound.clip = SoundGroup[1];
                Bgm_Sound.Play();
                break;
            case 2:
                Bgm_Sound.clip = SoundGroup[2];
                Bgm_Sound.Play();
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
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

    public void BtnClickSound()
    {
        SoundManager.instance.Effect_Sound_2.clip = SoundManager.instance.EffectGroup[14];
        SoundManager.instance.Effect_Sound_2.Play();
    }

}

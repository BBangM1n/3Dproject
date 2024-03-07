using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu; // 메뉴 오브젝트

    public AudioSource BgmSound; // 메인 월드 사운드 
    public AudioSource EffectSound; // 효과음 사운드

    public GameObject Muteimage; // 음소거 이미지
    bool mute; // 음소거 여부

    public Slider Volume;

    public GameObject Exit;
    GameManager manager;

    private void Awake()
    {
        BgmSound = GameObject.Find("Bgm_Sound").gameObject.GetComponent<AudioSource>();
        EffectSound = GameObject.Find("Effect_Sound").gameObject.GetComponent<AudioSource>();
        manager = GameObject.Find("Game Manager").gameObject.GetComponent<GameManager>();
    }
    void Start()
    {
        Volume.value = DataManager.instance.nowPlayer.SoundVolume;
    }


    void Update()
    {
        BgmSound.volume = Volume.value;
        EffectSound.volume = Volume.value;
    }

    public void MenuBtnClick()
    {
        Menu.SetActive(true);
    }

    public void MuteBtnClick()
    {
        mute = mute ? false : true;

        if(mute == true)
        {
            Muteimage.SetActive(true);
        }
        else if(mute == false)
        {
            Muteimage.SetActive(false);
        }
    }

    public void VolumeUpBtnClick() // 0~1
    {
        Volume.value += 0.1f;
        DataManager.instance.nowPlayer.SoundVolume = Volume.value;
        if (Volume.value >= 0.1f)
        {
            Muteimage.SetActive(false);
            mute = false;
        }
    }

    public void VolumeDownBtnClick()
    {
        Volume.value -= 0.1f;
        DataManager.instance.nowPlayer.SoundVolume = Volume.value;
        if (Volume.value <= 0f)
        {
            Volume.value = 0;
            Muteimage.SetActive(true);
            mute = true;
        }
    }

    public void CloseMenuBtnClick()
    {
        Menu.SetActive(false);
    }

    public void ExitBtnClick()
    {
        Menu.SetActive(false);
        manager.Pause();
    }

    public void SaveBtnClick()
    {
        DataManager.instance.SaveData();
        Debug.Log("세이브");
    }
}

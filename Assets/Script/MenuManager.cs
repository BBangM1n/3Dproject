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
    Player player;

    private void Awake()
    {
        BgmSound = GameObject.Find("Bgm_Sound").gameObject.GetComponent<AudioSource>();
        EffectSound = GameObject.Find("Effect_Sound").gameObject.GetComponent<AudioSource>();
        manager = GameObject.Find("Game Manager").gameObject.GetComponent<GameManager>();
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }
    void Start()
    {
        Volume.value = DataManager.instance.nowPlayer.SoundVolume; // 볼륨데이터 값 저장
    }


    void Update()
    {
        BgmSound.volume = Volume.value; // 볼륨데이터 값 전달
    }

    public void MenuBtnClick() // 메뉴 버튼 클릭
    {
        Menu.SetActive(true);
        player.isstop = true;
    }

    public void MuteBtnClick() // 음소거 버튼 클릭
    {
        mute = mute ? false : true;

        if(mute == true)
        {
            Muteimage.SetActive(true);
            Volume.value = 0;
        }
        else if(mute == false)
        {
            Muteimage.SetActive(false);
            Volume.value = DataManager.instance.nowPlayer.SoundVolume;
        }
    }

    public void VolumeUpBtnClick() // 0~1 볼륨 업 버튼 클릭
    {
        Volume.value += 0.1f;
        DataManager.instance.nowPlayer.SoundVolume = Volume.value;
        if (Volume.value >= 0.1f)
        {
            Muteimage.SetActive(false);
            mute = false;
        }
    }

    public void VolumeDownBtnClick() // 볼륨 다운 버튼 클릭
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

    public void CloseMenuBtnClick() // 메뉴 닫기 버튼 클릭
    {
        Menu.SetActive(false);
        player.isstop = false;
    }

    public void ExitBtnClick() // 나가기 버튼 클릭
    {
        Menu.SetActive(false);
        manager.Pause();
    }

    public void SaveBtnClick() // 세이브 버튼 클릭
    {
        DataManager.instance.SaveData();
    }
}

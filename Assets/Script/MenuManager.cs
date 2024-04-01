using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu; // �޴� ������Ʈ

    public AudioSource BgmSound; // ���� ���� ���� 
    public AudioSource EffectSound; // ȿ���� ����

    public GameObject Muteimage; // ���Ұ� �̹���
    bool mute; // ���Ұ� ����

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
        Volume.value = DataManager.instance.nowPlayer.SoundVolume; // ���������� �� ����
    }


    void Update()
    {
        BgmSound.volume = Volume.value; // ���������� �� ����
    }

    public void MenuBtnClick() // �޴� ��ư Ŭ��
    {
        Menu.SetActive(true);
        player.isstop = true;
    }

    public void MuteBtnClick() // ���Ұ� ��ư Ŭ��
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

    public void VolumeUpBtnClick() // 0~1 ���� �� ��ư Ŭ��
    {
        Volume.value += 0.1f;
        DataManager.instance.nowPlayer.SoundVolume = Volume.value;
        if (Volume.value >= 0.1f)
        {
            Muteimage.SetActive(false);
            mute = false;
        }
    }

    public void VolumeDownBtnClick() // ���� �ٿ� ��ư Ŭ��
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

    public void CloseMenuBtnClick() // �޴� �ݱ� ��ư Ŭ��
    {
        Menu.SetActive(false);
        player.isstop = false;
    }

    public void ExitBtnClick() // ������ ��ư Ŭ��
    {
        Menu.SetActive(false);
        manager.Pause();
    }

    public void SaveBtnClick() // ���̺� ��ư Ŭ��
    {
        DataManager.instance.SaveData();
    }
}

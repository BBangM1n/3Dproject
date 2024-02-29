using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    public int MaxHp = 100;          // 플레이어 최대 HP
    public int Ammo = 0;           // 플레이어 총알
    public int Gold = 0;           // 플레이어 골드
    public int Defens = 0;        // 플레이어 방어력
    public float PlayTime;     // 플레이 타임
    //public float SoundVolume;  // 사운드 볼륨
    public bool Weapon1 = false;       // 무기1
    public bool Weapon2 = false;       // 무기2
    public bool Weapon3 = false;       // 무기3
    public int EnhanceHead = 0;    // 강화 (모자)
    public int EnhanceArmor = 0;   // 강화 (방어구)
    public int EnhanceHammer = 0;  // 강화 (해머)
    public int EnhanceGun = 0;     // 강화 (총)
    public int MainQuestValue = 0; // 메인퀘스트 순서
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // 싱글톤패턴

    public PlayerData nowPlayer = new PlayerData(); // 플레이어 데이터 생성

    public string path; // 경로
    public int nowSlot; // 현재 슬롯번호
    public bool Tutorial = false;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save";	// 경로 지정
        print(path);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }

    public void DeleteData(int value)
    {
        File.Delete(path + value);
    }
}

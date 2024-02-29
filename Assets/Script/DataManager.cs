using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    public int MaxHp = 100;          // �÷��̾� �ִ� HP
    public int Ammo = 0;           // �÷��̾� �Ѿ�
    public int Gold = 0;           // �÷��̾� ���
    public int Defens = 0;        // �÷��̾� ����
    public float PlayTime;     // �÷��� Ÿ��
    //public float SoundVolume;  // ���� ����
    public bool Weapon1 = false;       // ����1
    public bool Weapon2 = false;       // ����2
    public bool Weapon3 = false;       // ����3
    public int EnhanceHead = 0;    // ��ȭ (����)
    public int EnhanceArmor = 0;   // ��ȭ (��)
    public int EnhanceHammer = 0;  // ��ȭ (�ظ�)
    public int EnhanceGun = 0;     // ��ȭ (��)
    public int MainQuestValue = 0; // ��������Ʈ ����
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance; // �̱�������

    public PlayerData nowPlayer = new PlayerData(); // �÷��̾� ������ ����

    public string path; // ���
    public int nowSlot; // ���� ���Թ�ȣ
    public bool Tutorial = false;

    private void Awake()
    {
        #region �̱���
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

        path = Application.persistentDataPath + "/save";	// ��� ����
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

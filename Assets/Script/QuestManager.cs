using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class QuestData
{
    public int QuestID;
    public string QuestTitle;
    public string ContentText;
    public bool Clear;
    public int Giftitem;
    public int Giftvalue;
}

[System.Serializable]
public class QuestDataList
{
    public List<QuestData> questDataList = new List<QuestData>();
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public QuestDataList questDataList = new QuestDataList(); // ����Ʈ ����� ������ ��ü

    string path;
    string filename = "save";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/";
    }

    private void Start()
    {
        print(path);
        QuestList();
        LoadData(); // ���� ���۵� �� ����� �����͸� �ҷ��ɴϴ�.
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(questDataList);
        File.WriteAllText(path + filename, data);
    }

    public void LoadData()
    {
        if (File.Exists(path + filename))
        {
            string data = File.ReadAllText(path + filename);
            questDataList = JsonUtility.FromJson<QuestDataList>(data);
        }
    }

    void QuestList()
    {
        QuestData newQuest1 = new QuestData();
        newQuest1.QuestID = 1;
        newQuest1.QuestTitle = "����Ʈ ����1";
        newQuest1.ContentText = "����Ʈ ����1";
        newQuest1.Clear = false;
        newQuest1.Giftitem = 2;
        newQuest1.Giftvalue = 100;
        questDataList.questDataList.Add(newQuest1);

        QuestData newQuest2 = new QuestData();
        newQuest2.QuestID = 2;
        newQuest2.QuestTitle = "����Ʈ ����2";
        newQuest2.ContentText = "����Ʈ ����2";
        newQuest2.Clear = false;
        newQuest2.Giftitem = 4;
        newQuest2.Giftvalue = 1000;
        questDataList.questDataList.Add(newQuest2);

        QuestData newQuest3 = new QuestData();
        newQuest3.QuestID = 3;
        newQuest3.QuestTitle = "����Ʈ ����3";
        newQuest3.ContentText = "����Ʈ ����3";
        newQuest3.Clear = false;
        newQuest3.Giftitem = 14;
        newQuest3.Giftvalue = 10200;
        questDataList.questDataList.Add(newQuest3);

        QuestData newQuest4 = new QuestData();
        newQuest4.QuestID = 4;
        newQuest4.QuestTitle = "����Ʈ ����4";
        newQuest4.ContentText = "����Ʈ ����4";
        newQuest4.Clear = false;
        newQuest4.Giftitem = 45;
        newQuest4.Giftvalue = 10200;
        questDataList.questDataList.Add(newQuest4);
    }
}

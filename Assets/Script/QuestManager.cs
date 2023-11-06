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

    public QuestDataList questDataList = new QuestDataList(); // 퀘스트 목록을 저장할 객체

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
        LoadData(); // 앱이 시작될 때 저장된 데이터를 불러옵니다.
        QuestList();
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
        newQuest1.QuestTitle = "퀘스트 제목1";
        newQuest1.ContentText = "퀘스트 내용1";
        newQuest1.Clear = false;
        newQuest1.Giftitem = 2;
        newQuest1.Giftvalue = 100;
        questDataList.questDataList.Add(newQuest1);

        QuestData newQuest2 = new QuestData();
        newQuest2.QuestID = 2;
        newQuest2.QuestTitle = "퀘스트 제목2";
        newQuest2.ContentText = "퀘스트 내용2";
        newQuest2.Clear = false;
        newQuest2.Giftitem = 4;
        newQuest2.Giftvalue = 1000;
        questDataList.questDataList.Add(newQuest2);

        SaveData();
    }
}

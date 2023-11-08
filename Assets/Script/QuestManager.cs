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
        QuestList();
        LoadData(); // 앱이 시작될 때 저장된 데이터를 불러옵니다.
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
        newQuest1.QuestTitle = "사냥을 해주세요";
        newQuest1.ContentText = "몬스터를 사냥해서 점수를 100점 이상 만들어주세요";
        newQuest1.Clear = false;
        newQuest1.Giftitem = 0;
        newQuest1.Giftvalue = 1000;
        questDataList.questDataList.Add(newQuest1);

        QuestData newQuest2 = new QuestData();
        newQuest2.QuestID = 2;
        newQuest2.QuestTitle = "총알을 넘겨주세요";
        newQuest2.ContentText = "총알을 100개 이상 들고있어주세요";
        newQuest2.Clear = false;
        newQuest2.Giftitem = 2;
        newQuest2.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest2);

        QuestData newQuest3 = new QuestData();
        newQuest3.QuestID = 3;
        newQuest3.QuestTitle = "몬스터를 처치해주세요";
        newQuest3.ContentText = "몬스터 한마리를 처치해주고 와주세요";
        newQuest3.Clear = false;
        newQuest3.Giftitem = 0;
        newQuest3.Giftvalue = 1300;
        questDataList.questDataList.Add(newQuest3);

        QuestData newQuest4 = new QuestData();
        newQuest4.QuestID = 4;
        newQuest4.QuestTitle = "수류탄 한개를 사용해주세요";
        newQuest4.ContentText = "수류탄 한개 사용";
        newQuest4.Clear = false;
        newQuest4.Giftitem = 3;
        newQuest4.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest4);
    }
}

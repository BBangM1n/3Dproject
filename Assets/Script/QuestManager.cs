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
        //LoadData(); // 앱이 시작될 때 저장된 데이터를 불러옵니다.
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
        newQuest1.QuestTitle = "마을의 일거리를 도와주세요!";
        newQuest1.ContentText = "몬스터가 너무 많아서 일을 할 수가 없어요.\n몬스터 좀 정리해주세요!";
        newQuest1.Clear = false;
        newQuest1.Giftitem = 0;
        newQuest1.Giftvalue = 1000;
        questDataList.questDataList.Add(newQuest1);

        QuestData newQuest2 = new QuestData();
        newQuest2.QuestID = 2;
        newQuest2.QuestTitle = "무기가 부족해";
        newQuest2.ContentText = "몬스터를 물리칠 총알이 부족한데\n 나에게 총알을 줄 수 있겠니?";
        newQuest2.Clear = false;
        newQuest2.Giftitem = 2;
        newQuest2.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest2);

        QuestData newQuest3 = new QuestData();
        newQuest3.QuestID = 3;
        newQuest3.QuestTitle = "몬스터가 너무 싫어";
        newQuest3.ContentText = "몬스터를 처치해주고 와주세요";
        newQuest3.Clear = false;
        newQuest3.Giftitem = 0;
        newQuest3.Giftvalue = 1300;
        questDataList.questDataList.Add(newQuest3);

        QuestData newQuest4 = new QuestData();
        newQuest4.QuestID = 4;
        newQuest4.QuestTitle = "폭발은 예술이야!!!";
        newQuest4.ContentText = "아무 수류탄을 한개 사용해줘!";
        newQuest4.Clear = false;
        newQuest4.Giftitem = 3;
        newQuest4.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest4);
    }
}

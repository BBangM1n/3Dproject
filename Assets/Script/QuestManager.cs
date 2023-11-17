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
        newQuest1.ContentText = "일이 너무 많아서 쉴 수가 없어요.\n일 좀 정리해주세요!";
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

        QuestData newQuest5 = new QuestData();
        newQuest5.QuestID = 5;
        newQuest5.QuestTitle = "나는 초록색이 싫어";
        newQuest5.ContentText = "초록색 공룡을 3마리만 물리쳐줄래?";
        newQuest5.Clear = false;
        newQuest5.Giftitem = 1;
        newQuest5.Giftvalue = 3000;
        questDataList.questDataList.Add(newQuest5);

        QuestData newQuest6 = new QuestData();
        newQuest6.QuestID = 6;
        newQuest6.QuestTitle = "농작물을 망치는 애들이 있어!";
        newQuest6.ContentText = "보라색 공룡들이 내 농작물을 망치고있어 \n 나 좀 도와줄래?";
        newQuest6.Clear = false;
        newQuest6.Giftitem = 2;
        newQuest6.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest6);

        QuestData newQuest7 = new QuestData();
        newQuest7.QuestID = 7;
        newQuest7.QuestTitle = "폭격이야!";
        newQuest7.ContentText = "노란색 공룡들의 미사일을 막아줘!";
        newQuest7.Clear = false;
        newQuest7.Giftitem = 3;
        newQuest7.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest7);

        QuestData newQuest8 = new QuestData();
        newQuest8.QuestID = 8;
        newQuest8.QuestTitle = "거대한 공룡";
        newQuest8.ContentText = "빨간색 거대한 공룡을 무찔러줘";
        newQuest8.Clear = false;
        newQuest8.Giftitem = 1;
        newQuest8.Giftvalue = 10000;
        questDataList.questDataList.Add(newQuest8);
    }
}

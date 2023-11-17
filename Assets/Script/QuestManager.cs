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
        //LoadData(); // ���� ���۵� �� ����� �����͸� �ҷ��ɴϴ�.
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
        newQuest1.QuestTitle = "������ �ϰŸ��� �����ּ���!";
        newQuest1.ContentText = "���� �ʹ� ���Ƽ� �� ���� �����.\n�� �� �������ּ���!";
        newQuest1.Clear = false;
        newQuest1.Giftitem = 0;
        newQuest1.Giftvalue = 1000;
        questDataList.questDataList.Add(newQuest1);

        QuestData newQuest2 = new QuestData();
        newQuest2.QuestID = 2;
        newQuest2.QuestTitle = "���Ⱑ ������";
        newQuest2.ContentText = "���͸� ����ĥ �Ѿ��� �����ѵ�\n ������ �Ѿ��� �� �� �ְڴ�?";
        newQuest2.Clear = false;
        newQuest2.Giftitem = 2;
        newQuest2.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest2);

        QuestData newQuest3 = new QuestData();
        newQuest3.QuestID = 3;
        newQuest3.QuestTitle = "���Ͱ� �ʹ� �Ⱦ�";
        newQuest3.ContentText = "���͸� óġ���ְ� ���ּ���";
        newQuest3.Clear = false;
        newQuest3.Giftitem = 0;
        newQuest3.Giftvalue = 1300;
        questDataList.questDataList.Add(newQuest3);

        QuestData newQuest4 = new QuestData();
        newQuest4.QuestID = 4;
        newQuest4.QuestTitle = "������ �����̾�!!!";
        newQuest4.ContentText = "�ƹ� ����ź�� �Ѱ� �������!";
        newQuest4.Clear = false;
        newQuest4.Giftitem = 3;
        newQuest4.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest4);

        QuestData newQuest5 = new QuestData();
        newQuest5.QuestID = 5;
        newQuest5.QuestTitle = "���� �ʷϻ��� �Ⱦ�";
        newQuest5.ContentText = "�ʷϻ� ������ 3������ �������ٷ�?";
        newQuest5.Clear = false;
        newQuest5.Giftitem = 1;
        newQuest5.Giftvalue = 3000;
        questDataList.questDataList.Add(newQuest5);

        QuestData newQuest6 = new QuestData();
        newQuest6.QuestID = 6;
        newQuest6.QuestTitle = "���۹��� ��ġ�� �ֵ��� �־�!";
        newQuest6.ContentText = "����� ������� �� ���۹��� ��ġ���־� \n �� �� �����ٷ�?";
        newQuest6.Clear = false;
        newQuest6.Giftitem = 2;
        newQuest6.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest6);

        QuestData newQuest7 = new QuestData();
        newQuest7.QuestID = 7;
        newQuest7.QuestTitle = "�����̾�!";
        newQuest7.ContentText = "����� ������� �̻����� ������!";
        newQuest7.Clear = false;
        newQuest7.Giftitem = 3;
        newQuest7.Giftvalue = 1;
        questDataList.questDataList.Add(newQuest7);

        QuestData newQuest8 = new QuestData();
        newQuest8.QuestID = 8;
        newQuest8.QuestTitle = "�Ŵ��� ����";
        newQuest8.ContentText = "������ �Ŵ��� ������ ������";
        newQuest8.Clear = false;
        newQuest8.Giftitem = 1;
        newQuest8.Giftvalue = 10000;
        questDataList.questDataList.Add(newQuest8);
    }
}
